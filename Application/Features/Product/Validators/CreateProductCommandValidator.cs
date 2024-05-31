using Application.Features.Product.Commands;
using Application.Features.Product.Constans;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ProductConstants.ProductName).NotNull()
                .MinimumLength(3).WithMessage(ProductConstants.ProductNameMinLength)
                .MaximumLength(50).WithMessage(ProductConstants.ProductNameMaxLength);
                

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(ProductConstants.ProductDescription).NotNull()
                .MinimumLength(10).WithMessage(ProductConstants.ProductDescriptionMinLength)
                .MaximumLength(100).WithMessage(ProductConstants.ProductDescriptionMaxLength);

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage(ProductConstants.ProductPrice).NotNull()
                .GreaterThan(0).WithMessage(ProductConstants.ProductQuantityGreaterThanZero);

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage(ProductConstants.ProductQuantity).NotNull()
                .GreaterThan(0).WithMessage(ProductConstants.ProductQuantityGreaterThanZero);
        }
    }
}
