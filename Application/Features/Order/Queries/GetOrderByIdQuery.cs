using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Order.Constants;
using Application.Features.Order.Models;
using Application.Features.Product.Constans;
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
    public class GetOrderByIdQuery : IRequest<GetOrderByIdResponse>
    {
        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
        {

            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

                var response = new GetOrderByIdResponse
                {
                    Id = order.Id,
                    OrderNumber = order.OrderNumber,
                    CustomerName = order.CustomerName,
                    DiscountAmount = order.DiscountAmount,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status.ToString(),
                    Products = order.Products.Select(p => new GetProductResponseWithOrder
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList(),
                    Address = new GetAddressResponseWithOrder
                    {
                        Address = order.Address.Address
                    },
                    User = new GetUserResponseWithOrder
                    {
                        FirstName = order.User.FirstName,
                        LastName = order.User.LastName,
                        Email = order.User.Email
                    },
                    CreatedDate = order.CreatedDate,
                };


                return response;
            }
        }
    }  
}
