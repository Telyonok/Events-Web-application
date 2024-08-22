using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Domain.Models
{
    public class EventParticipant
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Firstname { get; set; } = string.Empty;
        [Required]
        public string Lastname { get; set; } = string.Empty;
        [EmailAddress, Required]
        public string Email { get; set; } = string.Empty;
        public DateTime EventRegistrationDate { get; set; }
    }
}
