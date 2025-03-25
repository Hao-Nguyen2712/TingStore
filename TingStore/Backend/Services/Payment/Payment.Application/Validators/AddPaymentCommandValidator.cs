using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Payment.Application.Commands;

namespace Payment.Application.Validators
{
    public class AddPaymentCommandValidator : AbstractValidator<AddTransactionCommand>
    {
        public AddPaymentCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required").GreaterThan(0);
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required");
        }
    }
}
