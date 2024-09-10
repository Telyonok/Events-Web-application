using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Filters;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IAddEventUseCase
    {
        Task ExecuteAsync(CreateEventDTO createEventDTO);
    }

    public interface IGetAllEventsUseCase
    {
        Task<IEnumerable<Event>> ExecuteAsync();
    }

    public interface IGetEventByIdUseCase
    {
        Task<Event> ExecuteAsync(Guid id);
    }

    public interface IGetEventByTitleUseCase
    {
        Task<Event?> ExecuteAsync(string title);
    }

    public interface IGetEventsWithFilterUseCase
    {
        Task<IEnumerable<Event>> ExecuteAsync(EventFilter eventFilter);
    }

    public interface IGetAllEventsPagedUseCase
    {
        Task<PagedResult<Event>> ExecuteAsync(int page, int pageSize);
    }

    public interface IUpdateEventUseCase
    {
        Task ExecuteAsync(UpdateEventDTO updateEventDTO);
    }

    public interface IDeleteEventUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}
