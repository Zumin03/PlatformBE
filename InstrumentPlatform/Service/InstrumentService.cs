using InstrumentPlatform.Entities;
using InstrumentPlatform.Enums;
using InstrumentPlatform.Exceptions;
using InstrumentPlatform.Handlers;
using InstrumentPlatform.Model;
using Newtonsoft.Json;
using System.IO.Ports;

namespace InstrumentPlatform.Service
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IRepositoryService repositorySerice;
        private readonly ISerialCommunicationService serialCommunicationService;
        private readonly IInstrumentErrorHandler instrumentErrorHandler;
        private readonly ILogger<InstrumentService> logger;
        private readonly int defaultBaudRate = 9600;

        public InstrumentService(
            IRepositoryService repositoryService,
            ISerialCommunicationService serialCommunicationSerice,
            ILogger<InstrumentService> logger,
            IInstrumentErrorHandler instrumentErrorHandler)
        {
            this.repositorySerice = repositoryService;
            this.serialCommunicationService = serialCommunicationSerice;
            this.logger = logger;
            this.instrumentErrorHandler = instrumentErrorHandler;
        }

        /// <inheritdoc/>
        public async Task DetectInstrumentsAsync()
        {
            var activePorts = SerialPort.GetPortNames();
            await repositorySerice.ResetInstrumentsState();

            foreach (var port in activePorts)
            {
                try
                {
                    logger.LogInformation($"Searching for instrument on Serial Port: {port}");

                    var identificationResponse = serialCommunicationService.SendCommand(InstrumentCommand.Identify, port, defaultBaudRate, 1000);

                    var instrumentEntity = DeserializeInstrument(identificationResponse);
                    instrumentEntity.Port = port;
                    var isAuthorized = await repositorySerice.IsInstrumentAuthorized(instrumentEntity.DeviceId);
                    if (!isAuthorized)
                    {
                        throw new InstrumentNotAuthorizedException(instrumentEntity.DeviceId);
                    }

                    var selfTestResponse = RunSelfTestOnPort(port);

                    instrumentEntity.InstrumentState = selfTestResponse != null ? GetSelfTestResult(selfTestResponse) : InstrumentState.Faulted;

                    await repositorySerice.RegisterInstrument(instrumentEntity);
                }
                catch (TimeoutException)
                {
                    logger.LogInformation($"Serial Port {port} did not respond.");
                }
                catch (UnauthorizedAccessException)
                {
                    logger.LogError($"Access to {port} is denied.");
                }
                catch (FileNotFoundException ex)
                {
                    logger.LogError($"Something went wrong:\n\t{ex.Message}");
                }
                catch (InstrumentNotAuthorizedException ex)
                {
                    logger.LogWarning(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Something went wrong:\n\t{ex.Message}");
                }
            }
        }

        /// <inheritdoc/>
        public InstrumentEntity DeserializeInstrument(string instrumentJSON)
        {
            logger.LogInformation("Deserializing instrument JSON.");
            var instrumentEntity = JsonConvert.DeserializeObject<InstrumentEntity>(instrumentJSON);

            if (instrumentEntity == null)
            {
                throw new InvalidOperationException("Deserialization of instrument returned with null.");
            }

            return instrumentEntity; 
        }

        /// <inheritdoc/>
        public SelfTestDTO DeserializeSelfTest(string selfTestJSON)
        {
            logger.LogInformation("Deserializing self test JSON.");
            var selfTestDTO = JsonConvert.DeserializeObject<SelfTestDTO>(selfTestJSON);

            if (selfTestDTO == null)
            {
                throw new InvalidOperationException("Deserialization of self test returned with null.");
            }

            return selfTestDTO;
        }

        /// <inheritdoc/>
        public InstrumentState GetSelfTestResult(string selfTestJSON)
        {
            var selfTest = DeserializeSelfTest(selfTestJSON);
            if(selfTest.SensorState == "OK")
            {
                return InstrumentState.Connected;
            }
            else
            {
                return InstrumentState.Faulted;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<InstrumentDTO>> GetInstrumentsAsync()
        {
            var instruments = await repositorySerice.GetInstruments();
            var response = instruments.Select(MapInstrumentToDTO).ToList();
            return response;
        }

        /// <inheritdoc/>
        public async Task<InstrumentDTO> GetInstrumentAsync(string id)
        {
            var instruement = await repositorySerice.GetInstrumentById(id);
            var response = MapInstrumentToDTO(instruement);

            return response;
        }

        /// <inheritdoc/>
        public async Task<InstrumentDTO> RunSelfTest(string id)
        {
            logger.LogInformation($"Running self test on instrument with id: {id}");
            var instrument = await repositorySerice.GetInstrumentById(id);

            try
            {
                var selfTestResponse = RunSelfTestOnPort(instrument.Port);

                instrument.InstrumentState = selfTestResponse != null ? GetSelfTestResult(selfTestResponse) : InstrumentState.Faulted;

                await repositorySerice.RegisterInstrument(instrument);
                return MapInstrumentToDTO(instrument);
            }
            catch (FileNotFoundException)
            {
                await instrumentErrorHandler.HandleCommunicationError(id);
                throw new InstrumentCommunicationException(id);
            }
        }

        private InstrumentDTO MapInstrumentToDTO(InstrumentEntity instrument)
        {
            return new InstrumentDTO(
                instrument.DeviceId,
                instrument.DeviceName,
                instrument.Channel,
                instrument.SoftwareVersion,
                instrument.InstrumentState.ToString());

        }

        private string? RunSelfTestOnPort(string port)
        {
            logger.LogInformation($"Requesting self test on serial port: {port}");
            var selfTestResponse = serialCommunicationService.SendCommand(InstrumentCommand.SelfTest, port, 9600);

            return selfTestResponse;
        }
    }
}
