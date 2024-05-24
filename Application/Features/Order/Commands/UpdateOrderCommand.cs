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
    public class UpdateOrderCommand : IRequest
    {
        public UpdateOrderCommand(int id, int status)
        {
            Id = id;
            Status = status;
        }

        public int Id { get; }
        public int Status { get; }

        public class Handler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.FindAsync(request.Id);

                if (order is null)
                {
                    throw new NotFoundExcepiton("Sipariş Bulunamadı.");
                }

                // enumlar eklenikcek ilersi için şuan boş sipariş takibi için

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
