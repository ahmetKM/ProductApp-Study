using FluentValidation;
using ProductApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Validations
{
    public class ProductDtoValidatior:AbstractValidator<ProductDto>
    {
        public ProductDtoValidatior()
        {
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0).WithMessage("Price cannot be null and must be greater than 0");
            RuleFor(x => x.Stock).NotNull().NotEmpty().WithMessage("Stock cannot be null");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description cannot be null");
        }
    }
}
