using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.DTOs;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IAddEventParticipantUseCase
    {
        Task ExecuteAsync(CreateEventParticipantDTO createEventParticipantDTO);
    }

    public interface IGetAllEventParticipantsUseCase
    {
        Task<IEnumerable<EventParticipant>> ExecuteAsync();
    }

    public interface IGetEventParticipantByIdUseCase
    {
        Task<EventParticipant> ExecuteAsync(Guid id);
    }

    public interface IGetEventParticipantsByEventIdUseCase
    {
        Task<EventParticipant?> ExecuteAsync(Guid eventId);
    }

    public interface IGetAllEventParticipantsPagedUseCase
    {
        Task<PagedResult<EventParticipant>> ExecuteAsync(int page, int pageSize);
    }

    public interface IRegisterEventParticipantUseCase
    {
        Task ExecuteAsync(RegisterEventParticipantDto registerEventParticipantDto);
    }

    public interface IUnRegisterEventParticipantUseCase
    {
        Task ExecuteAsync(Guid eventParticipantId);
    }

    public interface IUpdateEventParticipantUseCase
    {
        Task ExecuteAsync(UpdateEventParticipantDTO updateEventParticipantDTO);
    }

    public interface IDeleteEventParticipantUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}
