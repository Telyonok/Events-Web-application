using AutoMapper;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;

namespace EventsWebApp.Application.UseCases.EventParticipantUseCases
{
    public class UpdateEventParticipantUseCase : IUpdateEventParticipantUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventParticipantUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(UpdateEventParticipantDTO updateEventParticipantDTO)
        {
            var eventParticipant = _mapper.Map<EventParticipant>(updateEventParticipantDTO);
            await _unitOfWork.EventParticipants.Upsert(eventParticipant);
            await _unitOfWork.CompleteAsync();
        }
    }
}
