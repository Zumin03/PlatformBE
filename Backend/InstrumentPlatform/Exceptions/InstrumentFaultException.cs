namespace InstrumentPlatform.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a measurement is attempted on a faulted instrument.
    /// </summary>
    public class InstrumentFaultException : Exception
    {
        public string DeviceId { get; }

        public InstrumentFaultException(string deviceId)
            : base($"Unable to run measurement on device {deviceId} because the instrument is faulted.")
        {
            DeviceId = deviceId;
        }
    }
}
