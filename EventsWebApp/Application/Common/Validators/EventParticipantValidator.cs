using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Common.Validators
{
    public class EventParticipantValidator : AbstractValidator<EventParticipant>
    {
        public EventParticipantValidator()
        {
            RuleFor(eventParticipant =>
                eventParticipant.Email).EmailAddress().NotEmpty().MaximumLength(50);
            RuleFor(eventParticipant =>
                eventParticipant.Firstname).NotEmpty().MaximumLength(20);
            RuleFor(eventParticipant =>
                eventParticipant.Lastname).NotEmpty().MaximumLength(20);
        }
    }
}