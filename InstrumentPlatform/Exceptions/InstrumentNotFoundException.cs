namespace InstrumentPlatform.Exceptions
{
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
