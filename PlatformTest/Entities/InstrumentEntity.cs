using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformTest.Entities
{
    public class InstrumentEntity
    {
        [Key]
        [Column("device_id")]
        public string DeviceId { get; set; }

        [Column("device_name")]
        public string DeviceName { get; set; }

        [Column("channel")]
        public string Channel { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("software_version")]
        public string SoftwareVersion { get; set; }

        [Column("comport")]
        public string Port { get; set; }

        [Column("is_connected")]
        public bool IsConnected { get; set; }

        [Column("is_operational")]
        public bool IsOperational { get; set; }

        protected InstrumentEntity() { }
    }
}