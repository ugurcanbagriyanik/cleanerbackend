using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cleaner.Entities
{
    public class PlayerWarMachine
    {
        [Key]
        public long Id { get; set; }
        public long PlayerCleanerId { get; set; }
        public string Path { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
        
        [ForeignKey("PlayerCleanerId")] public PlayerCleaner PlayerCleaner { get; set; }


        public virtual List<PlayerWarMachinePart> PlayerWarMachineParts { get; set; } =
            new List<PlayerWarMachinePart>();
    }
}
