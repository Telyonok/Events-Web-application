using AutoMapper;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using FluentValidation;

namespace EventsWebApp.Application.UseCases.EventUseCases
{
    public class UpdateEventUseCase : IUpdateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(UpdateEventDTO updateEventDTO)
        {
            var @event = _mapper.Map<Event>(updateEventDTO);
            await _unitOfWork.Events.Upsert(@event);
            await _unitOfWork.CompleteAsync();
        }
    }
}
