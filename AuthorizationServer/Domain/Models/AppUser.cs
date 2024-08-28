using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
