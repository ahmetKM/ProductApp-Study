using FluentValidation;
using ProductApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Validations
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name cannot be null");
        }
    }
}
