using AutoMapper;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using FluentValidation;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class UpdateEventParticipantUseCase : IUpdateEventParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<EventParticipant> _eventParticipantValidator;

        public UpdateEventParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper, IValidator<EventParticipant> eventParticipantValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventParticipantValidator = eventParticipantValidator;
        }

        public async Task ExecuteAsync(UpdateEventParticipantDTO updateEventParticipantDTO)
        {
            var eventParticipant = _mapper.Map<EventParticipant>(updateEventParticipantDTO);
            _eventParticipantValidator.ValidateAndThrow(eventParticipant);
            await _unitOfWork.EventParticipants.Upsert(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }
    }
}
