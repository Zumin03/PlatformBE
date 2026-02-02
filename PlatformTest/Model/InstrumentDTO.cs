namespace PlatformTest.Model
{
    public class InstrumentDTO
    {
        public InstrumentDTO(
            string deviceId,
            string deviceName,
            string channel,
            string softwareVersion,
            string port)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
            Channel = channel;
            SoftwareVersion = softwareVersion;
            Port = port;
        }

        public string DeviceId { get; }

        public string DeviceName { get; }

        public string Channel { get; }

        public string SoftwareVersion { get; }

        public string Port { get; set; }
    }
}
