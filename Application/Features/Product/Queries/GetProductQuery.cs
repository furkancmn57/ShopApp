using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Queries
{
    public class GetProductQuery : IRequest<List<ProductAggregate>>
    {
        public class Handler : IRequestHandler<GetProductQuery, List<ProductAggregate>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<ProductAggregate>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var products = await _context.Products.ToListAsync(cancellationToken);


                return products;
            }
        }
    }
}
