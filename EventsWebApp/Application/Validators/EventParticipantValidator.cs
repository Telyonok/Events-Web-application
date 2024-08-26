using EventsWebApp.Domain.Models;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class EventParticipantValidator : AbstractValidator<EventParticipant>
    {
        public EventParticipantValidator()
        {
            RuleFor(eventParticipant =>
                eventParticipant.Email)
                .EmailAddress().NotEmpty().WithMessage("Please enter a valid email address.")
                .MaximumLength(50).WithMessage("Email address cannot exceed 50 characters.");
            RuleFor(eventParticipant =>
                eventParticipant.Firstname).NotEmpty().WithMessage("First name is required.")
                .MaximumLength(20).WithMessage("First name cannot exceed 20 characters.");
            RuleFor(eventParticipant =>
                eventParticipant.Lastname).NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(20).WithMessage("Last name cannot exceed 20 characters.");
            RuleFor(eventParticipant =>
                eventParticipant.Birthdate).NotEmpty().Must(IsAValidBirthdate).WithMessage("Please enter a valid birthday.");
        }

        private bool IsAValidBirthdate(DateTime birthday)
        {
            var age = DateTime.Now.Year - birthday.Year;

            return age < 10 || age > 120;
        }
    }
}