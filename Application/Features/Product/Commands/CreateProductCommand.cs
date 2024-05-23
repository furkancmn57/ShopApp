using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest<ProductAggregate>
    {
        public CreateProductCommand(string name, string description, double price, List<Ingredients> ingredients, double quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Ingredients = ingredients;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand, ProductAggregate>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<ProductAggregate> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    throw new BusinessException("Ürün ismi boş olamaz.",400);
                }

                var product = ProductAggregate.Create(request.Name, request.Description, request.Price, request.Ingredients, request.Quantity);

                await _context.Products.AddAsync(product,cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }
    }
}
