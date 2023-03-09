using Cleaner.Enums;

namespace Cleaner.Models
{
    public class GeneratableBodyPartDTO
    {
        public int HolderTypeEnumId { get; set; } = (int)HolderTypeEnum.Top;
        public int Health { get; set; }
        public int Battery { get; set; }
        public int Attack { get; set; }
        public int BodyPartTypeEnumId { get; set; } = (int) BodyPartTypeEnum.Battery;
        public int Rarity { get; set; }
    }
}
