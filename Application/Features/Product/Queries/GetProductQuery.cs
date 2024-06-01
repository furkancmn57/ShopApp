using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
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
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Pagination<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
               var products = await _productRepository.GetAsync(request.Page, request.PageSize, cancellationToken);

               return products;
            }
        }
    }
}
