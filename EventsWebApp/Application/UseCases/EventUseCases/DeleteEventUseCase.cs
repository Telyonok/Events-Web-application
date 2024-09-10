using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class DeleteEventUseCase : IDeleteEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var result = await _unitOfWork.Events.Delete(id);
            if (!result)
                throw new NotFoundException("Event with provided id doesn't exist.");
            await _unitOfWork.CompleteAsync();
        }
    }
}
