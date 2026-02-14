namespace PlatformTest.Model
{
    public class InstrumentDTO
    {
        public InstrumentDTO(
            string deviceId,
            string deviceName,
            string channel,
            string softwareVersion,
            bool isOperational)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
            Channel = channel;
            SoftwareVersion = softwareVersion;
            IsOperational = isOperational;
        }

        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string Channel { get; set; }

        public string SoftwareVersion { get; set; }

        public bool IsOperational { get; set; }
    }
}
