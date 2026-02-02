using PlatformTest.Entities;
using PlatformTest.Model;

namespace PlatformTest.Service
{
    public interface IMeasurementService
    {
        /// <summary>
        /// Starts a measurement on the selected instrument.
        /// </summary>
        /// <returns>Returns a <see cref="MeasurementEntity"/>.</returns>
        Task<MeasurementDTO> RunMeasurementAsync(string deviceId);

        /// <summary>
        /// Deserializes the JSON file from a measurement to a MeasurementEntity object.
        /// </summary>
        /// <param name="measurementJSON">The raw JSON file from a measurement.</param>
        MeasurementEntity DeserializeMeasurement(string measurementJSON);

        /// <summary>
        /// Retrieves all recorded measurements and returns them in DTO format.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MeasurementDTO>> GetMeasurementsAsync();
    }
}
