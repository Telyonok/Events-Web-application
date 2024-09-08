namespace EventsWebApp.Domain.Models
{
    public class EventParticipant
    {
        public Guid Id { get; set; }
        public Guid? EventId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime EventRegistrationDate { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
