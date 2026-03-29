namespace InstrumentPlatform.Exceptions
{
    /// <summary>
    /// The exception that is thrown when communication with an instrument fails.
    /// </summary>
    /// <remarks>
    /// This exception typically occurs when a serial communication operation
    /// with the specified instrument is unsuccessful, such as in case of timeouts
    /// or unavailable ports.
    /// </remarks>
    public class InstrumentCommunicationException : Exception
    {
        public string DeviceId { get; }

        public InstrumentCommunicationException(string deviceId)
            : base($"Unable to communicate with instrument: {deviceId}")
        {
            DeviceId = deviceId;
        }
    }
}
