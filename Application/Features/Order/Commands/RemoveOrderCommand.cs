using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Order.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

                await _orderRepository.DeleteAsync(order, cancellationToken);
            }
        }
    }
}
