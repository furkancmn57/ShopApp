using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Commands.Validatators
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(p => p.AddressTitle).NotEmpty()
                .WithMessage("Lütfen Adres Başlığını giriniz.")
                .NotNull()
                .MaximumLength(50).WithMessage("Adres Başlığı maksimum 50 karakter olabilir.")
                .MinimumLength(3).WithMessage("Adress Başlığı minimum 3 karakter olabilir.");

            RuleFor(p=> p.Address).NotEmpty()
                .WithMessage("Lütfen Adres giriniz.")
                .NotNull()
                .MaximumLength(100).WithMessage("Adres maksimum 100 karakter olabilir.")
                .MinimumLength(10).WithMessage("Adress minimum 10 karakter olabilir.");
        }
    }
}
