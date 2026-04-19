using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentPlatform.Entities
{
    [Table("measurements")]
    public class MeasurementEntity
    {
        public MeasurementEntity(
            long id,
            string instrumentId,
            float value,
            DateTime measuredAt,
            InstrumentEntity instrument)
        {
            this.Id = id;
            this.InstrumentId = instrumentId;
            this.Value = value;
            this.MeasuredAt = measuredAt;
            this.Instrument = instrument;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("instrument_id")]
        [JsonProperty("instrumentId")]
        public string InstrumentId { get; set; } = null!;

        [Column("value")]
        [JsonProperty("value")]
        public float Value { get; set; }

        [Column("measured_at")]
        public DateTime MeasuredAt { get; set; }

        [ForeignKey(nameof(InstrumentId))]
        public virtual InstrumentEntity Instrument { get; set; } = null!;

        protected MeasurementEntity() { }
    }
}
