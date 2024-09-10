using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class GetEventParticipantsByEventIdUseCase : IGetEventParticipantsByEventIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventParticipantsByEventIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EventParticipant?> ExecuteAsync(Guid id)
        {
            var result = await _unitOfWork.EventParticipants.Find(e => e.EventId == id);
            return result.FirstOrDefault();
        }
    }
}
