using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Features.Product.Models;
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
    public class GetProductQuery : IRequest<Pagination<GetProductResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public class Handler : IRequestHandler<GetProductQuery, Pagination<GetProductResponse>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<Pagination<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var totalproducts = await _context.Products.CountAsync(cancellationToken);

                var products = await _context.Products
                    .Select(x => new GetProductResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Ingredients = x.Ingredients,
                        CreatedDate = x.CreatedDate
                    })
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);


                return new Pagination<GetProductResponse>
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalCount = totalproducts,
                    TotalPages = (int)Math.Ceiling(totalproducts / (decimal)request.PageSize),
                    Data = products
                };
            }
        }
    }
}
