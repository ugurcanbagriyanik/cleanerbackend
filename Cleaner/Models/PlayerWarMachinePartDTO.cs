
namespace Cleaner.Models
{
    public class PlayerWarMachinePartDTO
    {
        public long Id { get; set; }
        public long PlayerBodyPartId { get; set; }
        public long PlayerWarMachineId { get; set; }
        public char HolderId { get; set; } = 'a';
        
        public PlayerBodyPartDTO PlayerBodyPart { get; set; }
    }
}
