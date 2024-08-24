namespace EventsWebApp.Application.DTOs.EventParticipantDTOs
{
    public class CreateEventParticipantDTO
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
