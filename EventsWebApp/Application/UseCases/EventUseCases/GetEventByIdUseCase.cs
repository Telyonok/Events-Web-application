using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class GetEventByIdUseCase : IGetEventByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEventByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> ExecuteAsync(Guid id)
        {
            var result = await _unitOfWork.Events.GetById(id);
            if (result == null)
                throw new NotFoundException("Event not found.");
            return result;
        }
    }
}
