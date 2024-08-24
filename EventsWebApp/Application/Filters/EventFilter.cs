namespace EventsWebApp.Application.Filters
{
    public class EventFilter
    {
        public DateTime? EventDateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
