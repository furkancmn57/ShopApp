using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Mail.Enums;
using Application.Features.Mail.Models;
using Application.Features.Order.Constants;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public UpdateOrderCommand(int id, OrderStatus status)
        {
            Id = id;
            Status = status;
        }

        public int Id { get; }
        public OrderStatus Status { get; }

        public class Handler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly IShopAppDbContext _context;
            private readonly IMailProviderFactory _mailProviderFactory;

            public Handler(IShopAppDbContext context, IMailProviderFactory mailProviderFactory)
            {
                _context = context;
                _mailProviderFactory = mailProviderFactory;
            }

            public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Include(i => i.User).Where(x => x.Id == request.Id).AsQueryable().FirstOrDefaultAsync(cancellationToken);

                if (order is null)
                {
                    throw new NotFoundExcepiton(OrderConstants.OrderNotFound);
                }

                // enumlar eklenikcek ilersi için şuan boş sipariş takibi için

                order.Status = request.Status;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync(cancellationToken);

                var mailProvider = _mailProviderFactory.GetProvider(new Settings());
                await mailProvider.Send($"Siparişinizin durumu güncellendi. Yeni durum: {request.Status.ToString()}", "Sipariş Durumu Güncellendi", order.User.Email.ToString());
            }
        }
    }
}
