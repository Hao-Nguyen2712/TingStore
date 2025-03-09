// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using User.Application.Commands;

namespace User.Application.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("UserId must be greater than 0");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.FullName)
             .MaximumLength(100).WithMessage("Full name must not exceed 100 characters")
             .When(x => !string.IsNullOrEmpty(x.FullName));

            RuleFor(x => x.IsActive).Must(x => x == null).WithMessage("IsActive must be true or null");
        }
    }
}
