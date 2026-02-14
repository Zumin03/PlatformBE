using PlatformTest.Enums;

namespace PlatformTest.Model
{
    public class InstrumentDTO
    {
        public InstrumentDTO(
            string deviceId,
            string deviceName,
            string channel,
            string softwareVersion,
            string instrumentState)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
            Channel = channel;
            SoftwareVersion = softwareVersion;
            InstrumentState = instrumentState;
        }

        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string Channel { get; set; }

        public string SoftwareVersion { get; set; }

        public string InstrumentState { get; set; }
    }
}
