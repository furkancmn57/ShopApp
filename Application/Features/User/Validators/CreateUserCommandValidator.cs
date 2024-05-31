using Application.Features.User.Commands;
using Application.Features.User.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage(UserConstants.FirstNameNotEmpty).NotNull()
                .MinimumLength(3).WithMessage(UserConstants.FirstNameMinLength)
                .MaximumLength(50).WithMessage(UserConstants.FirstNameMaxLength);

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage(UserConstants.LastNameNotEmpty).NotNull()
                .MinimumLength(3).WithMessage(UserConstants.LastNameMinLength)
                .MaximumLength(50).WithMessage(UserConstants.LastNameMaxLength);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(UserConstants.EmailNotEmpty).NotNull()
                .EmailAddress().WithMessage(UserConstants.EmailAddress)
                .MaximumLength(50).WithMessage(UserConstants.EmailMaxLength);

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage(UserConstants.PasswordNotEmpty).NotNull()
                .MinimumLength(8).WithMessage(UserConstants.PasswordMinLength)
                .MaximumLength(64).WithMessage(UserConstants.PasswordMaxLength);
                
            //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,64}$")
            //.WithMessage("Şifreniz en az bir büyük harf, bir küçük harf, bir sayı ve bir özel karakter içermelidir.");
        }
    }
}
