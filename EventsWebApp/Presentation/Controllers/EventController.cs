using EventsWebApp.Application.DTOs;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Filters;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;

namespace EventsWebApp.Presentation.Controllers
{
    [ApiController]
    [Route("API/[controller]/[action]")]
    public class EventController : ControllerBase
    {
        private readonly IAddEventUseCase _addEventUseCase;
        private readonly IGetAllEventsUseCase _getAllEventsUseCase;
        private readonly IGetEventByIdUseCase _getEventByIdUseCase;
        private readonly IGetEventByTitleUseCase _getEventByTitleUseCase;
        private readonly IGetEventsWithFilterUseCase _getEventsWithFilterUseCase;
        private readonly IGetAllEventsPagedUseCase _getAllEventsPagedUseCase;
        private readonly IUpdateEventUseCase _updateEventUseCase;
        private readonly IDeleteEventUseCase _deleteEventUseCase;

        public EventController(
            IAddEventUseCase addEventUseCase,
            IGetAllEventsUseCase getAllEventsUseCase,
            IGetEventByIdUseCase getEventByIdUseCase,
            IGetEventByTitleUseCase getEventByTitleUseCase,
            IGetEventsWithFilterUseCase getEventsWithFilterUseCase,
            IGetAllEventsPagedUseCase getAllEventsPagedUseCase,
            IUpdateEventUseCase updateEventUseCase,
            IDeleteEventUseCase deleteEventUseCase)
        {
            _addEventUseCase = addEventUseCase;
            _getAllEventsUseCase = getAllEventsUseCase;
            _getEventByIdUseCase = getEventByIdUseCase;
            _getEventByTitleUseCase = getEventByTitleUseCase;
            _getEventsWithFilterUseCase = getEventsWithFilterUseCase;
            _getAllEventsPagedUseCase = getAllEventsPagedUseCase;
            _updateEventUseCase = updateEventUseCase;
            _deleteEventUseCase = deleteEventUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var result = await _getAllEventsUseCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventById(Guid id)
        {
            var result = await _getEventByIdUseCase.ExecuteAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Event>> GetEventByTitle(string title)
        {
            var result = await _getEventByTitleUseCase.ExecuteAsync(title);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetEventsWithFilter([FromQuery] EventFilter eventFilter)
        {
            var result = await _getEventsWithFilterUseCase.ExecuteAsync(eventFilter);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Event>>> GetAllEventsPaged(int page, int pageSize)
        {
            var result = await _getAllEventsPagedUseCase.ExecuteAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEvent([FromForm, AutoValidateAlways] CreateEventDTO createEventDTO)
        {
            await _addEventUseCase.ExecuteAsync(createEventDTO);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent([FromForm, AutoValidateAlways] UpdateEventDTO updateEventDTO)
        {
            await _updateEventUseCase.ExecuteAsync(updateEventDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _deleteEventUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}