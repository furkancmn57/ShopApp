using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands
{
    public class RemoveOrderCommand : IRequest
    {
        public RemoveOrderCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<RemoveOrderCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.FindAsync(request.Id);

                if (order is null)
                {
                    throw new NotFoundExcepiton("Sipariş Bulunamadı.");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
