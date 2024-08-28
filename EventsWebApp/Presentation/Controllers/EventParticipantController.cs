using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("API/[controller]/[action]")]
    public class EventParticipantController : ControllerBase
    {
        private readonly IEventParticipantService _eventParticipantService;
        public EventParticipantController(IEventParticipantService eventParticipantService)
        {
            _eventParticipantService = eventParticipantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEventParticipants()
        {
            var result = await _eventParticipantService.GetAllEventParticipants();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventParticipantById(Guid id)
        {
            var result = await _eventParticipantService.GetEventParticipantById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<Event>> GetEventParticipantsByEventId(Guid eventId)
        {
            var result = await _eventParticipantService.GetEventParticipantsByEventId(eventId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EventParticipant>>> GetAllEventParticipantsPaged(int page, int pageSize)
        {
            var result = await _eventParticipantService.GetAllEventParticipantsPaged(page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEventParticipant([FromBody] CreateEventParticipantDTO createEventParticipantDTO)
        {
            await _eventParticipantService.AddEventParticipant(createEventParticipantDTO);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEventParticipant([FromBody] UpdateEventParticipantDTO updateEventParticipantDTO)
        {
            await _eventParticipantService.UpdateEventParticipant(updateEventParticipantDTO);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterEventParticipant([FromBody] RegisterEventParticipantDto registerEventParticipantDto)
        {
            await _eventParticipantService.RegisterEventParticipant(registerEventParticipantDto);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnRegisterEventParticipant(Guid id)
        {
            await _eventParticipantService.UnRegisterEventParticipant(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEventParticipant(Guid id)
        {
            await _eventParticipantService.DeleteEventParticipant(id);
            return Ok();
        }
    }
}
