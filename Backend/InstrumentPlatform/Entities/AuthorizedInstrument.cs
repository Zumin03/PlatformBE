using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentPlatform.Entities
{
    [Table("authorized")]
    public class AuthorizedInstrument
    {
        [Column("instrument_id")]
        public string InstrumentId { get; set; } = null!;
    }
}
