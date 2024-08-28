using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEvents();

        Task<Event> GetEventById(Guid id);

        Task<Event?> GetEventByTitle(string title);

        Task<IEnumerable<Event>> GetEventsWithFilter(EventFilter eventFilter);
        Task<PagedResult<Event>> GetAllEventsPaged(int page, int pageSize);

        Task AddEvent(CreateEventDTO createEventDTO);

        Task UpdateEvent(UpdateEventDTO updateEventDTO);

        Task DeleteEvent(Guid id);
    }
}
