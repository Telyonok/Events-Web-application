using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApp.Infrastructure.Repositories
{
    public class EventParticipantRepository : GenericRepository<EventParticipant>, IEventParticipantRepository
    {
        public EventParticipantRepository(EventsDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<EventParticipant>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EventRepository));
                return new List<EventParticipant>();
            }
        }

        public override async Task<bool> Upsert(EventParticipant entity)
        {
            try
            {
                var existingEventParticipant = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingEventParticipant == null)
                    return await Add(entity);

                existingEventParticipant.Firstname = entity.Firstname;
                existingEventParticipant.Email = entity.Email;
                existingEventParticipant.Lastname = entity.Lastname;
                existingEventParticipant.Birthdate = entity.Birthdate;

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
