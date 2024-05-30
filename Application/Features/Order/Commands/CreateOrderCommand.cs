using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Tools;
using Application.Features.Order.Commands.Validators;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public CreateOrderCommand(int userId, int addressId, string customerName, List<int> productIds)
        {
            UserId = userId;
            AddressId = addressId;
            ProductIds = productIds;
            CustomerName = customerName;
        }

        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string CustomerName { get; set; }
        public List<int> ProductIds { get; set; }
        
        public class Handler : IRequestHandler<CreateOrderCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user is null)
                {
                    throw new NotFoundExcepiton("Kulanıcı Bulunamadı.");
                }

                var address = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == request.AddressId);

                if (address is null)
                {
                    throw new NotFoundExcepiton("Adres Bulunamadı.");
                }

                var products = await _context.Products.Where(x => request.ProductIds.Contains(x.Id)).ToListAsync(cancellationToken);

                if (products.Count != request.ProductIds.Count)
                {
                    throw new NotFoundExcepiton("Ürünler Bulunamadı.");
                }

                var validator = new CreateOrderCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException("Sipariş oluşturulurken hata oluştu.", validationResult.ToDictionary());
                }

                products.ForEach(x => x.Quantity--);
                double totalAmount = products.Sum(x => x.Price);

                var order = OrderAggregate.Create(totalAmount, 5, request.CustomerName, products, address, user);

                await _context.Orders.AddAsync(order, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
