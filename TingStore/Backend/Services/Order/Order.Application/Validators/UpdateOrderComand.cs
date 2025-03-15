using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Order.Application.Commands;

namespace Order.Application.Validators
{
   public class UpdateOrderComand : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderComand()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage("TotalAmount is required").GreaterThan(-1);
            RuleForEach(x => x.Item)
             .ChildRules(item =>
             {
                 item.RuleFor(x => x.Quantity)
                     .GreaterThanOrEqualTo(0)
                     .WithMessage("Quantity must be greater than 0");
             });
        }
   
    }
}
