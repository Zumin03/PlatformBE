namespace InstrumentPlatform.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an instrument with the specified device identifier cannot be found.
    /// </summary>
    public class InstrumentNotFoundException : Exception
    {
        public string DeviceId { get; }

        public InstrumentNotFoundException(string deviceId)
            : base($"Instrument not found with device id: {deviceId}")
        {
            DeviceId = deviceId;
        }
    }
}
