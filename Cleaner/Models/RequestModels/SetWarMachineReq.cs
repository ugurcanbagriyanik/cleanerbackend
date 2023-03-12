
namespace Cleaner.Models
{
    public class SetWarMachineReq
    {
        public long PlayerCleanerId { get; set; }
        public string Path { get; set; } = String.Empty;

        public List<SetWarMachinePartReq> PlayerWarMachineParts { get; set; } =
            new List<SetWarMachinePartReq>();
    }
    
    
    public class SetWarMachinePartReq
    {
        public long PlayerBodyPartId { get; set; }
        public char HolderId { get; set; } = 'a';
    }
}
