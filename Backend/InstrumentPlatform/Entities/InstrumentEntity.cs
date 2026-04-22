using InstrumentPlatform.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentPlatform.Entities
{
    [Table("instruments")]
    public class InstrumentEntity
    {
        public InstrumentEntity(
            string id,
            string name,
            string channel,
            string unit,
            string softwareVersion,
            string port,
            InstrumentState state)
        {
            this.Id = id;
            this.Name = name;
            this.Channel = channel;
            this.Unit = unit;
            this.SoftwareVersion = softwareVersion;
            this.Port = port;
            this.State = state;
        }

        [Key]
        [Column("id")]
        [JsonProperty("instrumentId")]
        public string Id { get; set; } = null!;

        [Column("name")]
        [JsonProperty("instrumentName")]
        public string Name { get; set; } = null!;

        [Column("channel")]
        public string Channel { get; set; } = null!;

        [Column("unit")]
        public string Unit { get; set; } = null!;

        [Column("software_version")]
        public string SoftwareVersion { get; set; } = null!;

        [Column("port")]
        public string Port { get; set; } = null!;

        [Column("state")]
        public InstrumentState State { get; set; }

        protected InstrumentEntity() { }
    }
}