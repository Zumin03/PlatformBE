using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformTest.Entities
{
    public class MeasurementEntity
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("deviceId")]
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; } = null!;

        [Column("value")]
        [JsonProperty("value")]
        public float Value { get; set; }

        [Column("measuredAt")]
        public DateTime MeasuredAt { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public virtual InstrumentEntity Instument { get; set; } = null!;

        protected MeasurementEntity() { }
    }
}
