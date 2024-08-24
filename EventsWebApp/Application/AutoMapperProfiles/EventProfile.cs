using AutoMapper;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.AutoMapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventDTO, Event>();
            CreateMap<UpdateEventDTO, Event>();
        }
    }
}
