using AutoMapper;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Helpers;
using EventsWebApp.Domain.Models;

namespace EventsWebApp.Application.AutoMapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventDTO, Event>().ForMember(dest => dest.Picture, opt => opt.MapFrom(src => FileToByteArrayConverter.Convert(src.Picture.FirstOrDefault()).ToString()));
            CreateMap<UpdateEventDTO, Event>().ForMember(dest => dest.Picture, opt => opt.MapFrom(src => FileToByteArrayConverter.Convert(src.Picture.FirstOrDefault()).ToString()));
        }
    }
}
