using Moq;
using Xunit;
using AutoMapper;
using FluentValidation;
using EventsWebApp.Domain.Repositories;
using EventsWebApp.Domain.Models;
using EventsWebApp.Application.UseCases; // Adjust this to your actual namespace
using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using System.Linq.Expressions;
using EventsWebApp.Application.Exceptions;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsWebApp.Application.Interfaces;
using EventsWebApp.Application.UseCases.EventParticipantUseCases;

public class EventParticipantUseCaseTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IValidator<EventParticipant>> _mockValidator;
    private readonly IAddEventParticipantUseCase _addEventParticipantUseCase;
    private readonly IGetAllEventParticipantsUseCase _getAllEventParticipantsUseCase;
    private readonly IGetEventParticipantByIdUseCase _getEventParticipantByIdUseCase;
    private readonly IGetEventParticipantsByEventIdUseCase _getEventParticipantsByEventIdUseCase;
    private readonly IGetAllEventParticipantsPagedUseCase _getAllEventParticipantsPagedUseCase;
    private readonly IUpdateEventParticipantUseCase _updateEventParticipantUseCase;
    private readonly IRegisterEventParticipantUseCase _registerEventParticipantUseCase;
    private readonly IUnRegisterEventParticipantUseCase _unRegisterEventParticipantUseCase;
    private readonly IDeleteEventParticipantUseCase _deleteEventParticipantUseCase;

    public EventParticipantUseCaseTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _mockValidator = new Mock<IValidator<EventParticipant>>();
  
        _addEventParticipantUseCase = new AddEventParticipantUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        _getAllEventParticipantsUseCase = new GetAllEventParticipantsUseCase(_mockUnitOfWork.Object);
        _getEventParticipantByIdUseCase = new GetEventParticipantByIdUseCase(_mockUnitOfWork.Object);
        _getEventParticipantsByEventIdUseCase = new GetEventParticipantsByEventIdUseCase(_mockUnitOfWork.Object);
        _getAllEventParticipantsPagedUseCase = new GetAllEventParticipantsPagedUseCase(_mockUnitOfWork.Object);
        _updateEventParticipantUseCase = new UpdateEventParticipantUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        _registerEventParticipantUseCase = new RegisterEventParticipantUseCase(_mockUnitOfWork.Object);
        _unRegisterEventParticipantUseCase = new UnRegisterEventParticipantUseCase(_mockUnitOfWork.Object);
        _deleteEventParticipantUseCase = new DeleteEventParticipantUseCase(_mockUnitOfWork.Object);
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
        await _addEventParticipantUseCase.ExecuteAsync(createDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Add(eventParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllEventParticipants_ReturnsParticipants()
    {
        // Arrange
        var participants = new List<EventParticipant> { new EventParticipant { Id = Guid.NewGuid(), Email = "test@example.com" } };
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.All()).ReturnsAsync(participants);

        // Act
        var result = await _getAllEventParticipantsUseCase.ExecuteAsync();

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
        var result = await _getEventParticipantByIdUseCase.ExecuteAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task GetEventParticipantsByEventId_ReturnsParticipants()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var participant = new EventParticipant { Id = Guid.NewGuid(), EventId = eventId };
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Find(It.IsAny<Expression<Func<EventParticipant, bool>>>()))
            .ReturnsAsync(new List<EventParticipant> { participant });

        // Act
        var result = await _getEventParticipantsByEventIdUseCase.ExecuteAsync(eventId);

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
        var result = await _getAllEventParticipantsPagedUseCase.ExecuteAsync(1, 1);

        // Assert
        Assert.Equal(2, result.TotalItems);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task UpdateEventParticipant_ShouldUpdateAndComplete()
    {
        // Arrange
        var updateDto = new UpdateEventParticipantDTO { Id = Guid.NewGuid(), Email = "updated@example.com" };
        var eventParticipant = new EventParticipant { Id = updateDto.Id, Email = "updated@example.com" };

        _mockMapper.Setup(m => m.Map<EventParticipant>(updateDto)).Returns(eventParticipant);
        _mockValidator.Setup(v => v.Validate(It.IsAny<EventParticipant>())).Returns(new ValidationResult());
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Upsert(eventParticipant));

        // Act
        await _updateEventParticipantUseCase.ExecuteAsync(updateDto);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Upsert(eventParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task RegisterEventParticipant_ShouldUpdateAndComplete()
    {
        // Arrange
        var registerDto = new RegisterEventParticipantDTO
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
        await _registerEventParticipantUseCase.ExecuteAsync(registerDto);

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
        await _unRegisterEventParticipantUseCase.ExecuteAsync(participantId);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.EventParticipants.Upsert(existingParticipant), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteEventParticipant_ShouldCallDeleteAndComplete()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockUnitOfWork.Setup(uow => uow.EventParticipants.Delete(id)).ReturnsAsync(true);

        // Act
        await _deleteEventParticipantUseCase.ExecuteAsync(id);

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
        await Assert.ThrowsAsync<NotFoundException>(() => _deleteEventParticipantUseCase.ExecuteAsync(id));
    }
}