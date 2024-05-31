using Application.Features.Address.Commands;
using Application.Features.Address.Constans;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Validatators
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(p => p.AddressTitle)
                .NotEmpty().WithMessage(AddressConstants.AddressTitle).NotNull()
                .MinimumLength(2).WithMessage(AddressConstants.AddressTitleMinLength)
                .MaximumLength(50).WithMessage(AddressConstants.AddressTitleMaxLength);
                

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage(AddressConstants.Address).NotNull()
                .MinimumLength(10).WithMessage(AddressConstants.AddressMinLength)
                .MaximumLength(100).WithMessage(AddressConstants.AddressMaxLength);
                
        }
    }
}
