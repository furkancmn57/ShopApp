using Application.Common.Interfaces;
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
    public class GetOrderQuery : IRequest<List<OrderAggregate>>
    {
        public class Handler : IRequestHandler<GetOrderQuery, List<OrderAggregate>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<OrderAggregate>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders.ToListAsync(cancellationToken);

                return orders;
            }
        }
    }
}
