namespace EventsWebApp.Application.DTOs.EventParticipantDTOs
{
    public class RegisterEventParticipantDTO
    {
        public Guid EventId { get; set; }
        public Guid EventParticipantId { get; set; }
    }
}
