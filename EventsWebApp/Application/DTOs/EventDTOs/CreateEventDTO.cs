namespace EventsWebApp.Application.DTOs.EventDTOs
{
    public class CreateEventDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDateTime { get; set; }
        public int MaxParticipants { get; set; }
        public string Picture { get; set; } = string.Empty; // String for now.
    }
}
