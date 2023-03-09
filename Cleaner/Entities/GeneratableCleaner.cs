using System.ComponentModel.DataAnnotations;

namespace Cleaner.Entities
{
    public class GeneratableCleaner
    {
        [Key]
        public int Id { get; set; }
        public string HolderSeed { get; set; } = string.Empty;
        public int DefaultHealth { get; set; }
        public int DefaultBattery { get; set; }
        public int Rarity { get; set; }
    }
}
