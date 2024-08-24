using EventsWebApp.Domain.Models;
using FluentValidation;

public class EventValidator : AbstractValidator<Event>
{
    public EventValidator()
    {
        RuleFor(@event =>
            @event.Title).NotEmpty().MaximumLength(20);
        RuleFor(@event =>
            @event.Description).MaximumLength(300);
        RuleFor(@event =>
            @event.Category).MaximumLength(20);
        RuleFor(@event =>
            @event.MaxParticipants).GreaterThan(0);
        RuleFor(@event =>
            @event.Location).MaximumLength(30);
    }
}