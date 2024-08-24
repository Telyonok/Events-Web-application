﻿using AutoMapper;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var result = await _unitOfWork.Events.All();
            return result;
        }

        public async Task<Event> GetEventById(Guid id)
        {
            var result = await _unitOfWork.Events.GetById(id);
            return result;
        }

        public async Task<Event?> GetEventByTitle(string title)
        {
            var result = await _unitOfWork.Events.Find(e => e.Title == title);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Event>> GetEventsWithFilter([FromBody] EventFilter eventFilter)
        {
            var result = await _unitOfWork.Events.GetEventsWithFilter(eventFilter);
            return result;
        }

        public async Task AddEvent([FromBody] CreateEventDTO createEventDTO)
        {
            var @event = _mapper.Map<Event>(createEventDTO);
            var result = await _unitOfWork.Events.Add(@event);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateEvent([FromBody] UpdateEventDTO updateEventDTO)
        {
            var @event = _mapper.Map<Event>(updateEventDTO);
            var result = await _unitOfWork.Events.Upsert(@event);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEvent(Guid id)
        {
            var result = await _unitOfWork.Events.Delete(id);
            if (!result)
                throw new NotFoundException("Event with provided id doesn't exists.");
            await _unitOfWork.CompleteAsync();
        }
    }
}