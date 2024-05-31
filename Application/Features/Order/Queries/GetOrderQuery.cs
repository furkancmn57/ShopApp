using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Features.Order.Models;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries
{
    public class GetOrderQuery : IRequest<Pagination<GetOrdersResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public class Handler : IRequestHandler<GetOrderQuery, Pagination<GetOrdersResponse>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<Pagination<GetOrdersResponse>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                var totalorders = await _context.Orders.CountAsync(cancellationToken);

                var orders = await _context.Orders
                    .Select(x => new GetOrdersResponse
                    {
                        Id = x.Id,
                        OrderNumber = x.OrderNumber,
                        CustomerName = x.CustomerName,
                        DiscountAmount = x.DiscountAmount,
                        TotalAmount = x.TotalAmount,
                        Status = x.Status.ToString(),
                        CreatedDate = x.CreatedDate,

                    })
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                return new Pagination<GetOrdersResponse>
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalCount = totalorders,
                    TotalPages = (int)Math.Ceiling(totalorders / (decimal)request.PageSize),
                    Data = orders
                };
            }
        }
    }
}
