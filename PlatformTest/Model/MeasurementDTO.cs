namespace PlatformTest.Model
{
    public class MeasurementDTO
    {
        public string DeviceName { get; set; }

        public string Channel { get; set; }

        public float Value { get; set; }

        public string Unit { get; set; }

        public DateTime MeasuredAt { get; set; }
    }
}
