using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Domain.Repositories
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsWithFilter(EventFilter filter);
    }
}
