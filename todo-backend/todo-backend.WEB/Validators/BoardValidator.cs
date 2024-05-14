using FluentValidation;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Validators
{
    public class BoardValidator : AbstractValidator<BoardDTO>
    {
        public BoardValidator()
        {
            RuleFor(c => c.Title).NotEmpty()
               .WithMessage("Board name is required")
               .MaximumLength(15).WithMessage("Board title cannot exceed 15 characters");
        }
    }
}
