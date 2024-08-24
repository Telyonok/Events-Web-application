namespace EventsWebApp.Application.DTOs.EventParticipantDTOs
{
    public class RegisterEventParticipantDto
    {
        public Guid EventId { get; set; }
        public Guid EventParticipantId { get; set; }
    }
}
