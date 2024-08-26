using AutoMapper;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Infrastructure.Repositories;
using FluentValidation;

namespace EventsWebApp.Application.Services
{
    public class EventParticipantService : IEventParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<EventParticipant> _eventParticipantValidator;
        public EventParticipantService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<EventParticipant> eventParticipant)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventParticipantValidator = eventParticipant;
        }

        public async Task AddEventParticipant(CreateEventParticipantDTO createEventParticipantDTO)
        {
            // Check if Event Participant wint provided email already exists.
            var foundExistingEvent = await _unitOfWork.EventParticipants.Find(
            ep => ep.Email == createEventParticipantDTO.Email);
            var existingEvent = foundExistingEvent.FirstOrDefault();
            if (existingEvent != null)
                throw new AlreadyExistsException("Event Participant with provided email already exists.");

            var eventParticipant = _mapper.Map<EventParticipant>(createEventParticipantDTO);
            _eventParticipantValidator.ValidateAndThrow(eventParticipant);
            var result = await _unitOfWork.EventParticipants.Add(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEventParticipant(Guid id)
        {
            var result = await _unitOfWork.EventParticipants.Delete(id);
            if (!result)
                throw new NotFoundException("Event Participant with provided id doesn't exists.");
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<EventParticipant>> GetAllEventParticipants()
        {
            var result = await _unitOfWork.EventParticipants.All();
            return result;
        }

        public async Task<EventParticipant> GetEventParticipantById(Guid id)
        {
            var result = await _unitOfWork.EventParticipants.GetById(id);
            return result;
        }

        public async Task<EventParticipant?> GetEventParticipantsByEventId(Guid eventId)
        {
            var result = await _unitOfWork.EventParticipants.Find(e => e.EventId == eventId);
            return result.FirstOrDefault();
        }

        public async Task RegisterEventParticipant(RegisterEventParticipantDto registerEventParticipantDto)
        {
            // Check if Event wint EventId exists.
            var foundExistingEvent = await _unitOfWork.Events.Find(
            e => e.Id == registerEventParticipantDto.EventId);
            var existingEvent = foundExistingEvent.FirstOrDefault();
            if (existingEvent == null)
                throw new NotFoundException("Event with provided Id does not exist.");

            // Check if EventParticipant wint EventParticipantId exists.
            var foundExistingEventParticipant = await _unitOfWork.EventParticipants.Find(
            ep => ep.Id == registerEventParticipantDto.EventParticipantId);
            var existingEventParticipant = foundExistingEventParticipant.FirstOrDefault();
            if (existingEventParticipant == null)
                throw new NotFoundException("Event participant with provided Id does not exist.");

            existingEventParticipant.EventId = registerEventParticipantDto.EventId;
            await _unitOfWork.EventParticipants.Upsert(existingEventParticipant);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UnRegisterEventParticipant(Guid eventParticipantId)
        {
            // Check if EventParticipant wint EventParticipantId exists.
            var foundExistingEventParticipant = await _unitOfWork.EventParticipants.Find(
            ep => ep.Id == eventParticipantId);
            var existingEventParticipant = foundExistingEventParticipant.FirstOrDefault();
            if (existingEventParticipant == null)
                throw new NotFoundException("Event participant with provided Id does not exist.");

            existingEventParticipant.EventId = Guid.Empty;
            await _unitOfWork.EventParticipants.Upsert(existingEventParticipant);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateEventParticipant(UpdateEventParticipantDTO updateEventParticipantDTO)
        {
            var eventParticipant = _mapper.Map<EventParticipant>(updateEventParticipantDTO);
            _eventParticipantValidator.ValidateAndThrow(eventParticipant);
            var result = await _unitOfWork.EventParticipants.Upsert(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }
    }
}
