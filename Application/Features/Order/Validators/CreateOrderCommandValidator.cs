using Application.Features.Order.Commands;
using Application.Features.Order.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(p => p.CustomerName)
                .NotEmpty().WithMessage(OrderConstants.CustomerName).NotNull()
                .MinimumLength(3).WithMessage(OrderConstants.CustomerNameMinLength)
                .MaximumLength(50).WithMessage(OrderConstants.CustomerNameMaxLength);
        }
    }
}
