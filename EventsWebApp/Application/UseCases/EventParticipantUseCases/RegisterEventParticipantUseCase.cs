using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class RegisterEventParticipantUseCase : IRegisterEventParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterEventParticipantUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(RegisterEventParticipantDTO registerEventParticipantDto)
        {
            var existingEvent = await _unitOfWork.Events.Find(e => e.Id == registerEventParticipantDto.EventId);
            if (!existingEvent.Any())
                throw new NotFoundException("Event with provided Id does not exist.");

            var existingEventParticipant = await _unitOfWork.EventParticipants.Find(ep => ep.Id == registerEventParticipantDto.EventParticipantId);
            if (!existingEventParticipant.Any())
                throw new NotFoundException("Event participant with provided Id does not exist.");

            var eventParticipant = existingEventParticipant.First();
            eventParticipant.EventId = registerEventParticipantDto.EventId;
            await _unitOfWork.EventParticipants.Upsert(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }
    }
}
