﻿namespace EventsWebApp.Application.DTOs.EventDTOs
{
    public class UpdateEventDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDateTime { get; set; }
        public int MaxParticipants { get; set; }
        public IFormFile[]? Picture { get; set; }
    }
}
