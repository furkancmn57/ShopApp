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
    public class UpdateProductCommand : IRequest
    {
        public UpdateProductCommand(int id,string name, string description, double price, List<Ingredients> ingredients, double quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Ingredients = ingredients;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public class Handler : IRequestHandler<UpdateProductCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Id);

                if (product is null)
                {
                    throw new BusinessException("Ürün Bulunamadı.", 404);
                }

                product.Update(request.Name, request.Description, request.Price, request.Ingredients, request.Quantity);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
