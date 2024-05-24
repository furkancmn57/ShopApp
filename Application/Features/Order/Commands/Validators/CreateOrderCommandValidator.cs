using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(p => p.CustomerName)
                .NotEmpty().WithMessage("Lütfen Müşteri Adını giriniz.").NotNull()
                .MaximumLength(50).WithMessage("Müşteri Adı maksimum 50 karakter olabilir.")
                .MinimumLength(3).WithMessage("Müşteri Adı minimum 3 karakter olabilir.");
        }
    }
}
