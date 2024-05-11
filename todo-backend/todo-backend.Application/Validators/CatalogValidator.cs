using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Application.Validators
{
    public class CatalogValidator : AbstractValidator<Catalog>
    {
        public CatalogValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Catalog name is required");
        }
    }

    
}
