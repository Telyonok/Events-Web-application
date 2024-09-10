using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class GetEventsWithFilterUseCase : IGetEventsWithFilterUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventsWithFilterUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> ExecuteAsync(EventFilter eventFilter)
        {
            return await _unitOfWork.Events.GetEventsWithFilter(eventFilter);
        }
    }
}
