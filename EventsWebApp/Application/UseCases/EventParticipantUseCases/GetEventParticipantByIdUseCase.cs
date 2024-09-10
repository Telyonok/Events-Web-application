using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class GetEventParticipantByIdUseCase : IGetEventParticipantByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventParticipantByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EventParticipant> ExecuteAsync(Guid id)
        {
            var result = await _unitOfWork.EventParticipants.GetById(id);
            if (result == null)
                throw new NotFoundException("Event Participant not found.");
            return result;
        }
    }
}
