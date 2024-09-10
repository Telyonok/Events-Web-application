using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class GetEventByTitleUseCase : IGetEventByTitleUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventByTitleUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event?> ExecuteAsync(string title)
        {
            var result = await _unitOfWork.Events.Find(e => e.Title == title);
            return result.FirstOrDefault();
        }
    }
}
