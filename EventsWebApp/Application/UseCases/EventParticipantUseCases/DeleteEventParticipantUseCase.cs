using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class DeleteEventParticipantUseCase : IDeleteEventParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventParticipantUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var result = await _unitOfWork.EventParticipants.Delete(id);
            if (!result)
                throw new NotFoundException("Event Participant with provided id doesn't exist.");
            await _unitOfWork.CompleteAsync();
        }
    }
}
