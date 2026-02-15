using Newtonsoft.Json;
using PlatformTest.Entities;
using PlatformTest.Enums;
using PlatformTest.Model;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO.Ports;

namespace PlatformTest.Service
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IRepositoryService repositorySerice;
        private readonly ILogger<InstrumentService> logger;

        private readonly int defaultTimeout = 1000;
        private readonly int defaultBaudRate = 9600;

        public InstrumentService(
            IRepositoryService repositoryService,
            ILogger<InstrumentService> logger)
        {
            this.repositorySerice = repositoryService;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task DetectInstrumentsAsync()
        {
            var activePorts = SerialPort.GetPortNames();
            await repositorySerice.ResetInstrumentsState();

            foreach (var port in activePorts)
            {
                using var serialPort = new SerialPort(port, defaultBaudRate);
                serialPort.Encoding = System.Text.Encoding.UTF8;
                logger.LogInformation($"Searching for instrument on Serial Port: {serialPort.PortName}");
                try
                {
                    serialPort.ReadTimeout = defaultTimeout;
                    serialPort.Open();
                    serialPort.WriteLine("IDENTIFY");
                    var identificationResponse = serialPort.ReadLine()?.Trim();

                    if (identificationResponse != null)
                    {
                        var instrumentEntity = DeserializeInstrument(identificationResponse);
                        instrumentEntity.Port = serialPort.PortName;

                        serialPort.WriteLine("SELFTEST");
                        var selfTestResponse = serialPort.ReadLine()?.Trim();

                        instrumentEntity.InstrumentState = selfTestResponse != null ? GetSelfTestResult(selfTestResponse) : InstrumentState.Faulted;

                        await repositorySerice.RegisterInstrument(instrumentEntity);
                    }
                    else
                    {
                        logger.LogInformation($"Unable to detect instrument on: {serialPort.PortName}");
                    }
                }
                catch (TimeoutException)
                {
                    logger.LogInformation($"Serial Port {serialPort.PortName} was not responding.");
                }
                catch (UnauthorizedAccessException)
                {
                    logger.LogError($"Access to {serialPort.PortName} is denied.");
                }
                catch (Exception e)
                {
                    logger.LogError($"Something went wrong:\n\t{e.Message}");
                }
                finally
                {
                    logger.LogInformation($"Closing Serial Port {serialPort.PortName}");
                    serialPort.Close();
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

        private InstrumentDTO MapInstrumentToDTO(InstrumentEntity instrument)
        {
            return new InstrumentDTO(
                instrument.DeviceId,
                instrument.DeviceName,
                instrument.Channel,
                instrument.SoftwareVersion,
                instrument.InstrumentState.ToString());

        }
    }
}
