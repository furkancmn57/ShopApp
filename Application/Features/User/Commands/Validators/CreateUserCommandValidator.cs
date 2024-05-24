using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("Lütfen Adınızı giriniz.").NotNull()
                .MaximumLength(50).WithMessage("Adınız maksimum 50 karakter olabilir.")
                .MinimumLength(3).WithMessage("Adınız minimum 3 karakter olabilir.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Lütfen Soyadınızı giriniz.").NotNull()
                .MaximumLength(50).WithMessage("Soyadınız maksimum 50 karakter olabilir.")
                .MinimumLength(3).WithMessage("Soyadınız minimum 3 karakter olabilir.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Lütfen E-Posta Adresinizi giriniz.").NotNull()
                .EmailAddress().WithMessage("Lütfen geçerli bir E-Posta Adresi giriniz.")
                .MaximumLength(50).WithMessage("E-Posta Adresiniz maksimum 50 karakter olabilir.")
                .MinimumLength(3).WithMessage("E-Posta Adresiniz minimum 3 karakter olabilir.");

        }
    }
}
