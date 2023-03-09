using System.ComponentModel.DataAnnotations;

namespace Cleaner.Entities
{
    public class Player
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(50, ErrorMessage = "Username length must be lower than 50 character!")]
        public string Username { get; set; } = string.Empty;
        public DateTimeOffset LastSeen { get; set; } = DateTimeOffset.MinValue;
        public DateTimeOffset FirstLogInDate { get; set; } = DateTimeOffset.Now;
        public string? PasswordHash { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }
        
        [MaxLength(200)]
        public string? MobileUserId { get; set; }
        public string? GooglePlayId { get; set; }
        public string? AppleId { get; set; }
        public string? FacebookId { get; set; }

        public bool? IsAndroid { get; set; }
        public bool? IsActive { get; set; }
    }
}
