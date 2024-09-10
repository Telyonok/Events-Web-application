using AutoMapper;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using FluentValidation;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class AddEventUseCase : IAddEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Event> _eventValidator;

        public AddEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Event> eventValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventValidator = eventValidator;
        }

        public async Task ExecuteAsync(CreateEventDTO createEventDTO)
        {
            var @event = _mapper.Map<Event>(createEventDTO);
            _eventValidator.ValidateAndThrow(@event);
            await _unitOfWork.Events.Add(@event);
            await _unitOfWork.CompleteAsync();
        }
    }
}
