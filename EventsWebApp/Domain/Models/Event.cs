using System.ComponentModel.DataAnnotations;

namespace EventsWebApp.Domain.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDateTime { get; set; }
        public int MaxParticipants { get; set; }
        public List<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
        public string? Picture { get; set; }
    }
}
