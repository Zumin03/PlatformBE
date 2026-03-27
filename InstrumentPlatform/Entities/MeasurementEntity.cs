using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentPlatform.Entities
{
    public class MeasurementEntity
    {
        public MeasurementEntity(
            long id,
            string deviceId,
            float value,
            DateTime measuredAt,
            InstrumentEntity instrument)
        {
            this.Id = id;
            this.DeviceId = deviceId;
            this.Value = value;
            this.MeasuredAt = measuredAt;
            this.Instument = instrument;
        }

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
