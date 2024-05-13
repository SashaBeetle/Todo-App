using FluentValidation;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Validators
{
    public class BoardValidator : AbstractValidator<BoardDTO>
    {
        public BoardValidator()
        {
            RuleFor(c => c.Title).NotEmpty()
               .WithMessage("Catalog name is required")
               .MaximumLength(15).WithMessage("Catalog title cannot exceed 15 characters");
        }
    }
}
