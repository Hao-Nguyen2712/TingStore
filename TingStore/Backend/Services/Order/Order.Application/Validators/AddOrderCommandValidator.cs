using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Order.Application.Commands;

namespace Order.Application.Validators
{
    public class AddOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public AddOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Items is required");
            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage("TotalAmount is required").GreaterThan(-1);
        }
    }
}
