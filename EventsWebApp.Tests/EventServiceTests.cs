using AutoMapper;
using EventsWebApp.Application.DTOs.EventDTOs;
using EventsWebApp.Application.Exceptions;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.UseCases.EventUseCases;
using EventsWebApp.Domain.Models;
using EventsWebApp.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace EventsWebApp.Tests
{
    public class EventUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IValidator<Event>> _mockValidator;
        private readonly IAddEventUseCase _addEventUseCase;
        private readonly IGetAllEventsUseCase _getAllEventsUseCase;
        private readonly IGetEventByIdUseCase _getEventByIdUseCase;
        private readonly IGetEventByTitleUseCase _getEventByTitleUseCase;
        private readonly IGetEventsWithFilterUseCase _getEventsWithFilterUseCase;
        private readonly IGetAllEventsPagedUseCase _getAllEventsPagedUseCase;
        private readonly IUpdateEventUseCase _updateEventUseCase;
        private readonly IDeleteEventUseCase _deleteEventUseCase;

        public EventUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<Event>>();

            // Initialize use cases
            _addEventUseCase = new AddEventUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
            _getAllEventsUseCase = new GetAllEventsUseCase(_mockUnitOfWork.Object);
            _getEventByIdUseCase = new GetEventByIdUseCase(_mockUnitOfWork.Object);
            _getEventByTitleUseCase = new GetEventByTitleUseCase(_mockUnitOfWork.Object);
            _getEventsWithFilterUseCase = new GetEventsWithFilterUseCase(_mockUnitOfWork.Object);
            _getAllEventsPagedUseCase = new GetAllEventsPagedUseCase(_mockUnitOfWork.Object);
            _updateEventUseCase = new UpdateEventUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
            _deleteEventUseCase = new DeleteEventUseCase(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllEvents_ReturnsAllEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Event1" },
                new Event { Id = Guid.NewGuid(), Title = "Event2" }
            };
            _mockUnitOfWork.Setup(uow => uow.Events.All()).ReturnsAsync(events);

            // Act
            var result = await _getAllEventsUseCase.ExecuteAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Event1", result.First().Title);
        }

        [Fact]
        public async Task GetEventById_ReturnsEvent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var eventToReturn = new Event { Id = id, Title = "Event1" };
            _mockUnitOfWork.Setup(uow => uow.Events.GetById(id)).ReturnsAsync(eventToReturn);

            // Act
            var result = await _getEventByIdUseCase.ExecuteAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Event1", result.Title);
        }

        [Fact]
        public async Task GetEventByTitle_ReturnsEvent()
        {
            // Arrange
            var title = "Event1";
            var eventToReturn = new List<Event> { new Event { Id = Guid.NewGuid(), Title = title } };
            _mockUnitOfWork.Setup(uow => uow.Events.Find(It.IsAny<Expression<Func<Event, bool>>>()))
                .ReturnsAsync(eventToReturn);

            // Act
            var result = await _getEventByTitleUseCase.ExecuteAsync(title);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
        }

        [Fact]
        public async Task GetAllEventsPaged_ReturnsPagedResult()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Event1" },
                new Event { Id = Guid.NewGuid(), Title = "Event2" }
            };
            _mockUnitOfWork.Setup(uow => uow.Events.All()).ReturnsAsync(events);

            // Act
            var result = await _getAllEventsPagedUseCase.ExecuteAsync(1, 1);

            // Assert
            Assert.Equal(2, result.TotalItems);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task AddEvent_ShouldCallAddAndComplete()
        {
            // Arrange
            var createEventDTO = new CreateEventDTO { Title = "New Event" };
            var eventToAdd = new Event { Title = "New Event" };
            _mockUnitOfWork.Setup(uow => uow.Events.Add(eventToAdd));
            _mockMapper.Setup(m => m.Map<Event>(createEventDTO)).Returns(eventToAdd);
            _mockValidator.Setup(v => v.Validate(It.IsAny<Event>())).Returns(new ValidationResult());

            // Act
            await _addEventUseCase.ExecuteAsync(createEventDTO);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Events.Add(eventToAdd), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteEvent_ShouldCallDeleteAndComplete()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.Events.Delete(id)).ReturnsAsync(true);

            // Act
            await _deleteEventUseCase.ExecuteAsync(id);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Events.Delete(id), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteEvent_ShouldThrowNotFoundException_WhenEventDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.Events.Delete(id)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _deleteEventUseCase.ExecuteAsync(id));
        }

        [Fact]
        public async Task UpdateEvent_ShouldUpdateAndComplete()
        {
            // Arrange
            var updateDto = new UpdateEventDTO { Id = Guid.NewGuid(), Title = "Updated Event" };
            var @event = new Event { Id = updateDto.Id, Title = "Updated Event" };

            _mockMapper.Setup(m => m.Map<Event>(updateDto)).Returns(@event);
            _mockValidator.Setup(v => v.Validate(It.IsAny<Event>())).Returns(new ValidationResult());
            _mockUnitOfWork.Setup(uow => uow.Events.Upsert(@event));

            // Act
            await _updateEventUseCase.ExecuteAsync(updateDto);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.Events.Upsert(@event), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}