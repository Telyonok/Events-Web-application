using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class GetAllEventParticipantsUseCase : IGetAllEventParticipantsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEventParticipantsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EventParticipant>> ExecuteAsync()
        {
            return await _unitOfWork.EventParticipants.All();
        }
    }
}
