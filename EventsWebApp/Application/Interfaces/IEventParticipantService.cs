using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.Interfaces
{
    public interface IEventParticipantService
    {
        Task<IEnumerable<EventParticipant>> GetAllEventParticipants();

        Task<EventParticipant> GetEventParticipantById(Guid id);
        Task<EventParticipant?> GetEventParticipantsByEventId(Guid eventId);

        Task AddEventParticipant(CreateEventParticipantDTO createEventParticipantDTO);

        Task UpdateEventParticipant(UpdateEventParticipantDTO updateEventParticipantDTO);

        Task DeleteEventParticipant(Guid id);
        Task RegisterEventParticipant(RegisterEventParticipantDto registerEventParticipantDto);
        Task UnRegisterEventParticipant(Guid unRegisterEventParticipantDto);
    }
}
