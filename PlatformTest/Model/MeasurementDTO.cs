namespace PlatformTest.Model
{
    public class MeasurementDTO
    {
        public MeasurementDTO(
            string deviceName,
            string channel,
            float value,
            string unit,
            DateTime measuredAt)
        {
            DeviceName = deviceName;
            Channel = channel;
            Value = value;
            Unit = unit;
            MeasuredAt = measuredAt;
        }

        public string DeviceName { get; set; }

        public string Channel { get; set; }

        public float Value { get; set; }

        public string Unit { get; set; }

        public DateTime MeasuredAt { get; set; }
    }
}
