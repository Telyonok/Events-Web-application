using EventsWebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Data
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
