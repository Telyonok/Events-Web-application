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

        public AddEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(CreateEventDTO createEventDTO)
        {
            var @event = _mapper.Map<Event>(createEventDTO);
            await _unitOfWork.Events.Add(@event);
            await _unitOfWork.CompleteAsync();
        }
    }
}
