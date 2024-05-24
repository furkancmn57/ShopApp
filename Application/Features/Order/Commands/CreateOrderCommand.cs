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
        public List<int> ProductIds { get; set; }
        public string CustomerName { get; set; }
        public virtual List<ProductAggregate> Products { get; set; }
        public virtual AddressAggregate Address { get; set; }
        public virtual UserAggregate User { get; set; }


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

                products.ForEach(x => x.Quantity--);
                await _context.SaveChangesAsync(cancellationToken);
                double totalAmount = products.Sum(x => x.Price);
                string orderNumber = await Tools.GenerateUniqueOrderNumber(cancellationToken);

                var validator = new CreateOrderCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException("Sipariş oluşturulurken hata oluştu.", validationResult.ToDictionary());
                }

                var order = OrderAggregate.Create(orderNumber, totalAmount, 0, request.CustomerName, products, address, user);
            }
        }
    }
}
