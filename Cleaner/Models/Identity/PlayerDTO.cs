using System.ComponentModel.DataAnnotations;
using SharedLibrary.Models;

namespace Cleaner.Models
{

    public class UserRequest
    {
        public string Username { get; set; }
        public string? AppleId { get; set; }
        public string? GooglePlayId { get; set; }
        public string? FacebookId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }

    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public long GemCount { get; set; }
        public long CoinCount { get; set; }


        public AuthenticateResponse(PlayerDTO player, string token)
        {
            Id = player.Id;
            Username = player.Username;
            Email = player.Email;
            Token = token;
            GemCount = player.GemCount;
            CoinCount = player.CoinCount;
        }
    }
}
