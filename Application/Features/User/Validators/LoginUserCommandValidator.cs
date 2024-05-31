﻿using Application.Features.User.Commands;
using Application.Features.User.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(UserConstants.EmailNotEmpty).NotNull();
            RuleFor(x => x.Password).NotEmpty().WithMessage(UserConstants.PasswordNotEmpty).NotNull();
        }
    }
}
