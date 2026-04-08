namespace InstrumentPlatform.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an instrument with the specified device identifier is not authorized in the database.
    /// </summary>
    public class InstrumentNotAuthorizedException : Exception
    {
        public string DeviceId { get; }

        public InstrumentNotAuthorizedException(string deviceId)
            : base($"Instrument with device id: {deviceId} is not authorized.")
        {
            DeviceId = deviceId;
        }
    }
}
