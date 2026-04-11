using System.ComponentModel.DataAnnotations.Schema;

namespace InstrumentPlatform.Entities
{
    [Table("authorized")]
    public class AuthorizedInstrumentEntity
    {
        public string DeviceId { get; set; } = null!;
    }
}
