using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EventsDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Event>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EventRepository));
                return new List<Event>();
            }
        }

        public override async Task<bool> Upsert(Event entity)
        {
            try
            {
                var existingEvent = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingEvent == null)
                    return await Add(entity);

                existingEvent.Category = entity.Category;
                existingEvent.MaxParticipants = entity.MaxParticipants;
                existingEvent.Picture = entity.Picture;
                existingEvent.Description = entity.Description;
                existingEvent.Title = entity.Title;
                existingEvent.Location = entity.Location;
                existingEvent.EventDateTime = entity.EventDateTime;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(EventRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(EventRepository));
                return false;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsWithFilter(EventFilter filter)
        {
            var result = dbSet.AsQueryable();

            if (filter.EventDateTime.HasValue)
                result = result.Where(e => e.EventDateTime == filter.EventDateTime.Value);

            if (!string.IsNullOrEmpty(filter.Category))
                result = result.Where(e => e.Category == filter.Category);

            if (!string.IsNullOrEmpty(filter.Location))
                result = result.Where(e => e.Location == filter.Location);

            return await result.ToListAsync();
        }
    }
}
