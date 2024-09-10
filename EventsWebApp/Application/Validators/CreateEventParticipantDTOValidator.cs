using EventsWebApp.Application.DTOs.EventParticipantDTOs;
using EventsWebApp.Application.Helpers;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class CreateEventParticipantDTOValidator : AbstractValidator<CreateEventParticipantDTO>
    {
        public CreateEventParticipantDTOValidator()
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
                eventParticipant.Birthdate).NotEmpty().Must(DateHelpers.IsAValidBirthdate).WithMessage("Please enter a valid birthday.");
        }
    }
}
