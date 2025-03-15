using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Order.Application.Commands;

namespace Order.Application.Validators
{
   public class UpdateStatusOrderCommandValidator : AbstractValidator<UpdateStatusOrderCommand>
    {
        public UpdateStatusOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        }
    }
}
