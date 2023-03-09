using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cleaner.Entities
{
    public class PlayerCleaner
    {
        [Key]
        public long Id { get; set; }
        public int GeneratableCleanerId { get; set; }
        public long PlayerId { get; set; }
        public bool IsActive { get; set; } = true;
        
        [ForeignKey("GeneratableCleanerId")] public GeneratableCleaner GeneratableCleaner { get; set; }
        [ForeignKey("PlayerId")] public Player Player { get; set; }
    }
}
