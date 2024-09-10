using AutoMapper;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Domain.Models;
using FluentValidation;
using EventsWebApp.Application.Interfaces;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class AddEventParticipantUseCase : IAddEventParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddEventParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(CreateEventParticipantDTO input)
        {
            var foundExistingEvent = await _unitOfWork.EventParticipants.Find(ep => ep.Email == input.Email);
            if (foundExistingEvent.Any())
                throw new AlreadyExistsException("Event Participant with provided email already exists.");

            var eventParticipant = _mapper.Map<EventParticipant>(input);
            await _unitOfWork.EventParticipants.Add(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }
    }
}
