using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
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
            private readonly IOrderRepository _orderRepository;
            private readonly IMailProviderFactory _mailProviderFactory;
            public Handler(IOrderRepository orderRepository, IMailProviderFactory mailProviderFactory)
            {
                _orderRepository = orderRepository;
                _mailProviderFactory = mailProviderFactory;
            }

            public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

                order.Status = request.Status;

                await _orderRepository.UpdateAsync(order, cancellationToken);

                var mailProvider = _mailProviderFactory.GetProvider(new Settings());
                await mailProvider.Send($"Siparişinizin durumu güncellendi. Yeni durum: {request.Status}", "Sipariş Durumu Güncellendi", order.User.Email.ToString());
            }
        }
    }
}
