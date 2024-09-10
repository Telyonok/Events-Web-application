using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class GetAllEventsUseCase : IGetAllEventsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEventsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> ExecuteAsync()
        {
            return await _unitOfWork.Events.All();
        }
    }
}
