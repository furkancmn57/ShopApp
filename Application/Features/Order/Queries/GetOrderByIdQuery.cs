using Application.Common.Exceptions;
using Application.Common.Interfaces;
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
    public class GetOrderByIdQuery : IRequest<GetOrderByIdResponse>
    {
        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders
                    .Include(i => i.Products)
                    .Include(i => i.Address)
                    .Include(i => i.User)
                    .Where(x => x.Id == request.Id)
                    .Select(order => new GetOrderByIdResponse
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
                    }).FirstOrDefaultAsync(cancellationToken);

                if (order is null)
                {
                    throw new NotFoundExcepiton("Sipariş Bulunamadı.");
                }

                return order;
            }
        }
    }  
}
