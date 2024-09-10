using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class GetAllEventsPagedUseCase : IGetAllEventsPagedUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEventsPagedUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<Event>> ExecuteAsync(int page, int pageSize)
        {
            var totalEvents = await _unitOfWork.Events.All();
            var events = totalEvents
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Event>
            {
                TotalItems = totalEvents.Count(),
                CurrentPage = page,
                PageSize = pageSize,
                Items = events
            };
        }
    }
}
