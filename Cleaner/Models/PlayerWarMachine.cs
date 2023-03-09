
namespace Cleaner.Models
{
    public class PlayerWarMachineDTO
    {
        public long Id { get; set; }
        public long PlayerCleanerId { get; set; }
        public string Path { get; set; } = String.Empty;
        
        public PlayerCleanerDTO PlayerCleaner { get; set; }


        public List<PlayerWarMachinePartDTO> PlayerWarMachineParts { get; set; } =
            new List<PlayerWarMachinePartDTO>();
    }
}
