using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş olamaz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.");
        }
    }
}
