using Moq;
using Xunit;
using AutoMapper;
using FluentValidation;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Domain.Models;
using EventsWebApp.Application.Services;
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using System.Linq.Expressions;
using EventsWebApp.Application.Exceptions;
using FluentValidation.Results;

public class EventParticipantServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IValidator<EventParticipant>> _mockValidator;
    private readonly EventParticipantService _eventParticipantService;

    public EventParticipantServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _mockValidator = new Mock<IValidator<EventParticipant>>();
        _eventParticipantService = new EventParticipantService(_mockMapper.Object, _mockUnitOfWork.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task AddEventParticipant_ShouldAddAndComplete()
    {
        // Arrange
        var createDto = new CreateEventParticipantDTO { Email = "test@example.com" };
        var eventParticipant = new EventParticipant { Email = "test@example.com" };

        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Find(It.IsAny<Expression<Func<EventParticipant, bool>>>()))
            .ReturnsAsync(new List<EventParticipant>());
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Add(eventParticipant));
        _mockMapper.Setup(m => m.Map<EventParticipant>(createDto)).Returns(eventParticipant);
        _mockValidator.Setup(v => v.Validate(It.IsAny<EventParticipant>())).Returns(new ValidationResult());

        // Act
        await _eventParticipantService.AddEventParticipant(createDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Add(eventParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task AddEventParticipant_ShouldThrowAlreadyExistsException_WhenEmailExists()
    {
        // Arrange
        var createDto = new CreateEventParticipantDTO { Email = "test@example.com" };
        var existingParticipant = new EventParticipant { Email = "test@example.com" };

        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Find(It.IsAny<Expression<Func<EventParticipant, bool>>>()))
            .ReturnsAsync(new List<EventParticipant> { existingParticipant });

        // Act & Assert
        await Assert.ThrowsAsync<AlreadyExistsException>(() => _eventParticipantService.AddEventParticipant(createDto));
    }

    [Fact]
    public async Task DeleteEventParticipant_ShouldCallDeleteAndComplete()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Delete(id)).ReturnsAsync(true);

        // Act
        await _eventParticipantService.DeleteEventParticipant(id);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Delete(id), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteEventParticipant_ShouldThrowNotFoundException_WhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Delete(id)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _eventParticipantService.DeleteEventParticipant(id));
    }

    [Fact]
    public async Task GetAllEventParticipants_ReturnsParticipants()
    {
        // Arrange
        var participants = new List<EventParticipant> { new EventParticipant { Id = Guid.NewGuid(), Email = "test@example.com" } };
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.All()).ReturnsAsync(participants);

        // Act
        var result = await _eventParticipantService.GetAllEventParticipants();

        // Assert
        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async Task GetEventParticipantById_ReturnsParticipant()
    {
        // Arrange
        var id = Guid.NewGuid();
        var participant = new EventParticipant { Id = id, Email = "test@example.com" };
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.GetById(id)).ReturnsAsync(participant);

        // Act
        var result = await _eventParticipantService.GetEventParticipantById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task GetEventParticipantsByEventId_ReturnsParticipant()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var participant = new EventParticipant { Id = Guid.NewGuid(), EventId = eventId };
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Find(It.IsAny<Expression<Func<EventParticipant, bool>>>()))
            .ReturnsAsync(new List<EventParticipant> { participant });

        // Act
        var result = await _eventParticipantService.GetEventParticipantsByEventId(eventId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(eventId, result.EventId);
    }

    [Fact]
    public async Task GetAllEventParticipantsPaged_ReturnsPagedResult()
    {
        // Arrange
        var participants = new List<EventParticipant>
        {
            new EventParticipant { Id = Guid.NewGuid(), Email = "test1@example.com" },
            new EventParticipant { Id = Guid.NewGuid(), Email = "test2@example.com" }
        };
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.All()).ReturnsAsync(participants);

        // Act
        var result = await _eventParticipantService.GetAllEventParticipantsPaged(1, 1);

        // Assert
        Assert.Equal(2, result.TotalItems);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task RegisterEventParticipant_ShouldUpdateAndComplete()
    {
        // Arrange
        var registerDto = new RegisterEventParticipantDto
        {
            EventId = Guid.NewGuid(),
            EventParticipantId = Guid.NewGuid()
        };
        var existingEvent = new Event { Id = registerDto.EventId };
        var existingParticipant = new EventParticipant { Id = registerDto.EventParticipantId };

        _mockUnitOfWork.Setup(uow => uow.Events.Find(It.IsAny<Expression<Func<Event, bool>>>()))
            .ReturnsAsync(new List<Event> { existingEvent });
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Find(It.IsAny<Expression<Func<EventParticipant, bool>>>()))
            .ReturnsAsync(new List<EventParticipant> { existingParticipant });

        // Act
        await _eventParticipantService.RegisterEventParticipant(registerDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Upsert(existingParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UnRegisterEventParticipant_ShouldUpdateAndComplete()
    {
        // Arrange
        var participantId = Guid.NewGuid();
        var existingParticipant = new EventParticipant { Id = participantId };

        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Find(It.IsAny<Expression<Func<EventParticipant, bool>>>()))
            .ReturnsAsync(new List<EventParticipant> { existingParticipant });

        // Act
        await _eventParticipantService.UnRegisterEventParticipant(participantId);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Upsert(existingParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateEventParticipant_ShouldUpdateAndComplete()
    {
        // Arrange
        var updateDto = new UpdateEventParticipantDTO { Id = Guid.NewGuid(), Email = "updated@example.com" };
        var eventParticipant = new EventParticipant { Id = updateDto.Id, Email = "updated@example.com" };

        _mockMapper.Setup(m => m.Map<EventParticipant>(updateDto)).Returns(eventParticipant);
        _mockValidator.Setup(v => v.Validate(It.IsAny <EventParticipant>())).Returns(new ValidationResult());
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Add(eventParticipant));

        // Act
        await _eventParticipantService.UpdateEventParticipant(updateDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Upsert(eventParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }
}