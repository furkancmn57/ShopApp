using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Tools;
using Application.Features.Address.Constans;
using Application.Features.Order.Commands;
using Application.Features.Order.Constants;
using Application.Features.Order.Validators;
using Application.Features.Product.Constans;
using Application.Features.User.Constants;
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
            private readonly IOrderRepository _orderRepository;
            private readonly IAddressRepository _addressRepository;
            private readonly IUserRepository _userRepository;
            private readonly IProductRepository _productRepository;

            public Handler(IOrderRepository orderRepository, IAddressRepository addressRepository, IProductRepository productRepository, IUserRepository userRepository)
            {
                _orderRepository = orderRepository;
                _addressRepository = addressRepository;
                _productRepository = productRepository;
                _userRepository = userRepository;
            }

            public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

                var address = await _addressRepository.GetByIdAsync(request.AddressId, cancellationToken);

                var products = new List<ProductAggregate>();
                for (int i = 0; i < request.ProductIds.Count; i++)
                {
                    var product = await _productRepository.GetByIdAsync(request.ProductIds[i], cancellationToken);
                    if (product is null)
                    {
                        throw new NotFoundExcepiton(ProductConstants.ProductNotFound);
                    }
                    products.Add(product);
                }

                if (products.Count != request.ProductIds.Count)
                {
                    throw new NotFoundExcepiton(ProductConstants.ProductNotFound);
                }
                
                products.ForEach(x => x.Quantity--);

                if (products.Any(x => x.Quantity < 0))
                {
                    throw new BusinessException(ProductConstants.ProductQuantityGreaterThanZero);
                }

                double totalAmount = products.Sum(x => x.Price);

                var validator = new CreateOrderCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid is false)
                {
                    throw new ValidationException(OrderConstants.OrderAddError, validationResult.ToDictionary());
                }

                var order = OrderAggregate.Create(totalAmount, 0, request.CustomerName, products, address, user);
                await _orderRepository.CreateAsync(order, cancellationToken);
            }
        }
    }
}
