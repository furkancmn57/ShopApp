using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderAggregate>
    {
        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetOrderByIdQuery, OrderAggregate>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<OrderAggregate> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.FindAsync(request.Id,cancellationToken);

                if (order is null)
                {
                    throw new NotFoundExcepiton("Sipariş Bulunamadı.");
                }

                return order;
            }
        }
    }  
}
