using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cleaner.Entities
{
    public class PlayerBodyPart
    {
        [Key]
        public long Id { get; set; }
        public int GeneratableBodyPartId { get; set; }
        public long PlayerId { get; set; }
        public bool IsActive { get; set; } = true;
        
        [ForeignKey("GeneratableBodyPartId")] public GeneratableBodyPart GeneratableBodyPart { get; set; }
        [ForeignKey("PlayerId")] public Player Player { get; set; }
    }
}
