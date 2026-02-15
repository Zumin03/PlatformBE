using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlatformTest.Entities;
using PlatformTest.Exceptions;
using PlatformTest.Model;
using System.IO.Ports;
using System.Threading.Channels;

namespace PlatformTest.Service
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IRepositoryService repositoryService;
        private readonly ILogger<IMeasurementService> logger;

        public MeasurementService(
            IRepositoryService repositoryService,
            ILogger<IMeasurementService> logger)
        {
            this.repositoryService = repositoryService;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<MeasurementDTO> RunMeasurementAsync(string deviceId)
        {
            var instrument = await repositoryService.GetInstrumentById(deviceId);

            logger.LogInformation($"Starting measurement on device {instrument.DeviceId}");
            using var serial = new SerialPort(instrument.Port, 9600);
            serial.Open();
            serial.WriteLine("MEASURE");
            var line = serial.ReadLine().Trim();
            serial.Close();

            var result = DeserializeMeasurement(line);
            await repositoryService.SaveMeasurement(result);

            return MapMeasurementToDTO(result);
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
