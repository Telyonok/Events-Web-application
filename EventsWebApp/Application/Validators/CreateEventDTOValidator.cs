using EventsWebApp.Application.DTOs.EventDTOs;
using FluentValidation;

namespace EventsWebApp.Application.Validators
{
    public class CreateEventDTOValidator : AbstractValidator<CreateEventDTO>
    {
        public CreateEventDTOValidator()
        {
            RuleFor(@event =>
                @event.Title).NotEmpty().WithMessage("Title is required.")
                .MaximumLength(20).WithMessage("Title cannot exceed 20 characters.");
            RuleFor(@event =>
                @event.Description).MaximumLength(300).WithMessage("Description cannot exceed 300 characters.");
            RuleFor(@event =>
                @event.Category).MaximumLength(20).WithMessage("Category cannot exceed 20 characters.");
            RuleFor(@event =>
                @event.MaxParticipants).GreaterThan(0).WithMessage("MaxParticipants cannot be less than 0.");
            RuleFor(@event =>
                @event.Location).MaximumLength(30).WithMessage("Location cannot exceed 20 characters.");
        }
    }
}
