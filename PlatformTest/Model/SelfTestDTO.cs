namespace PlatformTest.Model
{
    public class SelfTestDTO
    {
        public SelfTestDTO(
            string deviceId,
            string sensorState)
        {
            DeviceId = deviceId;
            SensorState = sensorState;
        }

        public string DeviceId { get; set; }

        public string SensorState { get; set; }
    }
}
