using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Lütfen Ürün Adını giriniz.").NotNull()
                .MaximumLength(50).WithMessage("Ürün Adı maksimum 50 karakter olabilir.")
                .MinimumLength(3).WithMessage("Ürün Adı minimum 3 karakter olabilir.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Lütfen Ürün Açıklamasını giriniz.").NotNull()
                .MaximumLength(100).WithMessage("Ürün Açıklaması maksimum 100 karakter olabilir.")
                .MinimumLength(10).WithMessage("Ürün Açıklaması minimum 10 karakter olabilir.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Lütfen Ürün Fiyatını giriniz.").NotNull()
                .GreaterThan(0).WithMessage("Ürün Fiyatı 0'dan büyük olmalıdır.");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Lütfen Ürün Stok miktarını giriniz.").NotNull()
                .GreaterThan(0).WithMessage("Ürün Stok miktarı 0'dan büyük olmalıdır.");
        }
    }
}
