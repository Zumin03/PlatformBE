using PlatformTest.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformTest.Entities
{
    public class InstrumentEntity(
        string deviceId,
        string deviceName,
        string channel,
        string unit,
        string softwareVersion,
        string port,
        InstrumentState instrumentState)
    {
        [Key]
        [Column("device_id")]
        public string DeviceId { get; set; } = deviceId;

        [Column("device_name")]
        public string DeviceName { get; set; } = deviceName;

        [Column("channel")]
        public string Channel { get; set; } = channel;

        [Column("unit")]
        public string Unit { get; set; } = unit;

        [Column("software_version")]
        public string SoftwareVersion { get; set; } = softwareVersion;

        [Column("comport")]
        public string Port { get; set; } = port;

        [Column("state")]
        public InstrumentState InstrumentState { get; set; } = instrumentState;
    }
}