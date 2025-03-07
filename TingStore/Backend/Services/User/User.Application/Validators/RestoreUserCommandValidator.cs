
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using User.Application.Commands;

namespace User.Application.Validators
{
    public class RestoreUserCommandValidator : AbstractValidator<RestoreUserCommand>
    {
        public RestoreUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("UserId must be greater than 0");
        }
    }
}
