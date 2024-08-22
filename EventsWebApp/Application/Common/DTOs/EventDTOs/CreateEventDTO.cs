using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Common.DTOs
{
    public class CreateEventDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public string Picture { get; set; } = string.Empty; // String for now.
    }
}
