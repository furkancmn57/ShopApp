using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Mail.Enums;
using Application.Features.Mail.Models;
using Application.Features.Order.Constants;
using Domain.Enums;
using Domain.Models;
using MassTransit;
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
            private readonly IPublishEndpoint _publishEndpoint;

            public Handler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
            {
                _orderRepository = orderRepository;
                _publishEndpoint = publishEndpoint;
            }

            public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

                order.Status = request.Status;

                await _orderRepository.UpdateAsync(order, cancellationToken);

                await _publishEndpoint.Publish(new UpdateOrderMessage
                {
                    Email = order.User.Email,
                    Status = order.Status.ToString()
                }, cancellationToken);
            }
        }
    }
}
