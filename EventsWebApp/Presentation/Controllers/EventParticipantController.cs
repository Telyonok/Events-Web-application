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
        private readonly IAddEventParticipantUseCase _addEventParticipantUseCase;
        private readonly IGetAllEventParticipantsUseCase _getAllEventParticipantsUseCase;
        private readonly IGetEventParticipantByIdUseCase _getEventParticipantByIdUseCase;
        private readonly IGetEventParticipantsByEventIdUseCase _getEventParticipantsByEventIdUseCase;
        private readonly IGetAllEventParticipantsPagedUseCase _getAllEventParticipantsPagedUseCase;
        private readonly IUpdateEventParticipantUseCase _updateEventParticipantUseCase;
        private readonly IRegisterEventParticipantUseCase _registerEventParticipantUseCase;
        private readonly IUnRegisterEventParticipantUseCase _unRegisterEventParticipantUseCase;
        private readonly IDeleteEventParticipantUseCase _deleteEventParticipantUseCase;

        public EventParticipantController(
            IAddEventParticipantUseCase addEventParticipantUseCase,
            IGetAllEventParticipantsUseCase getAllEventParticipantsUseCase,
            IGetEventParticipantByIdUseCase getEventParticipantByIdUseCase,
            IGetEventParticipantsByEventIdUseCase getEventParticipantsByEventIdUseCase,
            IGetAllEventParticipantsPagedUseCase getAllEventParticipantsPagedUseCase,
            IUpdateEventParticipantUseCase updateEventParticipantUseCase,
            IRegisterEventParticipantUseCase registerEventParticipantUseCase,
            IUnRegisterEventParticipantUseCase unRegisterEventParticipantUseCase,
            IDeleteEventParticipantUseCase deleteEventParticipantUseCase)
        {
            _addEventParticipantUseCase = addEventParticipantUseCase;
            _getAllEventParticipantsUseCase = getAllEventParticipantsUseCase;
            _getEventParticipantByIdUseCase = getEventParticipantByIdUseCase;
            _getEventParticipantsByEventIdUseCase = getEventParticipantsByEventIdUseCase;
            _getAllEventParticipantsPagedUseCase = getAllEventParticipantsPagedUseCase;
            _updateEventParticipantUseCase = updateEventParticipantUseCase;
            _registerEventParticipantUseCase = registerEventParticipantUseCase;
            _unRegisterEventParticipantUseCase = unRegisterEventParticipantUseCase;
            _deleteEventParticipantUseCase = deleteEventParticipantUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventParticipant>>> GetAllEventParticipants()
        {
            var result = await _getAllEventParticipantsUseCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventParticipant>> GetEventParticipantById(Guid id)
        {
            var result = await _getEventParticipantByIdUseCase.ExecuteAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<List<EventParticipant>>> GetEventParticipantsByEventId(Guid eventId)
        {
            var result = await _getEventParticipantsByEventIdUseCase.ExecuteAsync(eventId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EventParticipant>>> GetAllEventParticipantsPaged(int page, int pageSize)
        {
            var result = await _getAllEventParticipantsPagedUseCase.ExecuteAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEventParticipant([FromBody] CreateEventParticipantDTO createEventParticipantDTO)
        {
            await _addEventParticipantUseCase.ExecuteAsync(createEventParticipantDTO);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEventParticipant([FromBody] UpdateEventParticipantDTO updateEventParticipantDTO)
        {
            await _updateEventParticipantUseCase.ExecuteAsync(updateEventParticipantDTO);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterEventParticipant([FromBody] RegisterEventParticipantDto registerEventParticipantDto)
        {
            await _registerEventParticipantUseCase.ExecuteAsync(registerEventParticipantDto);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnRegisterEventParticipant(Guid id)
        {
            await _unRegisterEventParticipantUseCase.ExecuteAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEventParticipant(Guid id)
        {
            await _deleteEventParticipantUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}