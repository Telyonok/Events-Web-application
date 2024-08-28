using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public string TokenString { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
