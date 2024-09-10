using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class GetAllEventParticipantsPagedUseCase : IGetAllEventParticipantsPagedUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllEventParticipantsPagedUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<EventParticipant>> ExecuteAsync(int page, int pageSize)
        {
            var totalEventParticipants = await _unitOfWork.EventParticipants.All();
            var eventParticipants = totalEventParticipants
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedResult<EventParticipant>
            {
                TotalItems = totalEventParticipants.Count(),
                CurrentPage = page,
                PageSize = pageSize,
                Items = eventParticipants
            };
        }
    }
}
