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
    }
}
