using System.ComponentModel.DataAnnotations;
using Cleaner.Enums;

namespace Cleaner.Entities
{
    public class GeneratableBodyPart
    {
        [Key]
        public int Id { get; set; }
        public int HolderTypeEnumId { get; set; } = (int)HolderTypeEnum.Top;
        public int Health { get; set; }
        public int Battery { get; set; }
        public int Attack { get; set; }
        public int BodyPartTypeEnumId { get; set; } = (int) BodyPartTypeEnum.Battery;
        public int Rarity { get; set; }
    }
}
