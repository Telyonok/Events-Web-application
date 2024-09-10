using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class UnRegisterEventParticipantUseCase : IUnRegisterEventParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnRegisterEventParticipantUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid eventParticipantId)
        {
            var existingEventParticipant = await _unitOfWork.EventParticipants.Find(ep => ep.Id == eventParticipantId);
            if (!existingEventParticipant.Any())
                throw new NotFoundException("Event participant with provided Id does not exist.");

            var eventParticipant = existingEventParticipant.First();
            eventParticipant.EventId = Guid.Empty;
            await _unitOfWork.EventParticipants.Upsert(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }
    }
}
