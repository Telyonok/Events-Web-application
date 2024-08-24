using AutoMapper;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.AutoMapperProfiles
{
    public class EventParticipantProfile : Profile
    {
        public EventParticipantProfile()
        {
            CreateMap<CreateEventParticipantDTO, EventParticipant>();
            CreateMap<UpdateEventParticipantDTO, EventParticipant>();
        }
    }
}
