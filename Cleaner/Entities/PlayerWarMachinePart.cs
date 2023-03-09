using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cleaner.Entities
{
    public class PlayerWarMachinePart
    {
        [Key]
        public long Id { get; set; }
        public long PlayerBodyPartId { get; set; }
        public long PlayerWarMachineId { get; set; }
        public char HolderId { get; set; } = 'a';
        public bool IsActive { get; set; } = true;
        
        [ForeignKey("PlayerBodyPartId")] public PlayerBodyPart PlayerBodyPart { get; set; }
        [ForeignKey("PlayerWarMachineId")] public PlayerWarMachine PlayerWarMachine { get; set; }
    }
}
