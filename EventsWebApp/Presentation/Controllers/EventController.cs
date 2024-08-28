using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("API/[controller]/[action]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var result = await _eventService.GetAllEvents();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventById(Guid id)
        {
            var result = await _eventService.GetEventById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Event>> GetEventByTitle(string title)
        {
            var result = await _eventService.GetEventByTitle(title);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetEventsWithFilter([FromQuery] EventFilter eventFilter)
        {
            var result = await _eventService.GetEventsWithFilter(eventFilter);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEvent([FromBody] CreateEventDTO createEventDTO)
        {
            await _eventService.AddEvent(createEventDTO);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDTO updateEventDTO)
        {
            await _eventService.UpdateEvent(updateEventDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEvent(id);
            return Ok();
        }
    }
}
