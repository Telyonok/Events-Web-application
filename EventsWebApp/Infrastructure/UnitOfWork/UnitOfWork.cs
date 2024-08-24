using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using EventsWebApp.Infrastructure.Repositories;

namespace EventsWebApp.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EventsDbContext _context;
        private readonly ILogger _logger;

        public IEventRepository Events { get; private set; }
        public IEventParticipantRepository EventParticipants { get; private set; }

        public UnitOfWork(EventsDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Events = new EventRepository(context, _logger);
            EventParticipants = new EventParticipantRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
