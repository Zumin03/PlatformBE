namespace InstrumentPlatform.Model
{
    public class InstrumentDTO
    {
        public InstrumentDTO(
            string deviceId,
            string deviceName,
            string channel,
            string softwareVersion,
            string instrumentState,
            string unit)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
            Channel = channel;
            SoftwareVersion = softwareVersion;
            InstrumentState = instrumentState;
            Unit = unit;
        }

        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string Channel { get; set; }

        public string SoftwareVersion { get; set; }

        public string InstrumentState { get; set; }

        public string Unit { get; set; }
    }
}
