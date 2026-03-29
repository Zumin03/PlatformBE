using InstrumentPlatform.Entities;
using InstrumentPlatform.Service;
using Newtonsoft.Json;
using InstrumentPlatform.Exceptions;
using InstrumentPlatform.Model;
using System.IO.Ports;
using System.Threading.Channels;
using InstrumentPlatform.Enums;
using Microsoft.OpenApi.Extensions;
using InstrumentPlatform.Handlers;

namespace InstrumentPlatform.Service
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IRepositoryService repositoryService;
        private readonly ISerialCommunicationService serialCommunicationService;
        private readonly ILogger<IMeasurementService> logger;
        private readonly IInstrumentService instrumentService;
        private readonly IInstrumentErrorHandler instrumentErrorHandler;

        public MeasurementService(
            IRepositoryService repositoryService,
            ISerialCommunicationService serialCommunicationService,
            ILogger<IMeasurementService> logger,
            IInstrumentService instrumentService,
            IInstrumentErrorHandler instrumentErrorHandler)
        {
            this.repositoryService = repositoryService;
            this.serialCommunicationService = serialCommunicationService;
            this.logger = logger;
            this.instrumentService = instrumentService;
            this.instrumentErrorHandler = instrumentErrorHandler;
        }

        /// <inheritdoc/>
        public async Task<MeasurementDTO> RunMeasurementAsync(string deviceId)
        {
            var instrument = await repositoryService.GetInstrumentById(deviceId);

            logger.LogInformation($"Starting measurement on device {instrument.DeviceId}");
            try
            {
                var line = serialCommunicationService.SendCommand(InstrumentCommand.Measure, instrument.Port, 9600);

                var result = DeserializeMeasurement(line);
                await repositoryService.SaveMeasurement(result);

                return MapMeasurementToDTO(result);
            }
            catch (FileNotFoundException)
            {
                await instrumentErrorHandler.HandleCommunicationError(deviceId);
                throw new InstrumentCommunicationException(deviceId);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MeasurementDTO>> GetMeasurementsAsync()
        {
            var measurements = await repositoryService.GetMeasurements();

            var response = measurements.Select(MapMeasurementToDTO).ToList();
            return response;
        }

        /// <inheritdoc/>
        public MeasurementEntity DeserializeMeasurement(string measurementJSON)
        {
            var result = JsonConvert.DeserializeObject<MeasurementEntity>(measurementJSON)
                ?? throw new DeserializationException(measurementJSON);
            return result;
        }

        private MeasurementDTO MapMeasurementToDTO(MeasurementEntity measurement)
        {
            return new MeasurementDTO(
                measurement.Instument.DeviceName,
                measurement.Instument.Channel,
                measurement.Value,
                measurement.Instument.Unit,
                measurement.MeasuredAt);
        }
    }
}
