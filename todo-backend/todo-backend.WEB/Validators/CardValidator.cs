using FluentValidation;
using todo_backend.WEB.Mapping.DTOs;
namespace todo_backend.WEB.Validators
{
    public class CardValidator : AbstractValidator<CardDTO>
    {
        public CardValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Card title is required")
                .MaximumLength(12).WithMessage("Card title cannot exceed 12 characters"); 

            RuleFor(c => c.Description).NotEmpty()
                .WithMessage("Card description is required")
                .MaximumLength(256).WithMessage("Card title cannot exceed 12 characters");

            RuleFor(c => c.Priority).NotEmpty()
                .WithMessage("Card priority is required");

            RuleFor(c => c.DueDate).NotEmpty()
                .WithMessage("Card due date is required");
        }
    }
}
