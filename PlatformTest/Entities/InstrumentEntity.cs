using PlatformTest.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformTest.Entities
{
    public class InstrumentEntity
    {
        [Key]
        [Column("device_id")]
        public string DeviceId { get; set; } = null!;

        [Column("device_name")]
        public string DeviceName { get; set; } = null!;

        [Column("channel")]
        public string Channel { get; set; } = null!;

        [Column("unit")]
        public string Unit { get; set; } = null!;

        [Column("software_version")]
        public string SoftwareVersion { get; set; } = null!;

        [Column("comport")]
        public string Port { get; set; } = null!;

        [Column("state")]
        public InstrumentState InstrumentState { get; set; }

        protected InstrumentEntity() { }
    }
}