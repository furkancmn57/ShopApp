﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Product.Constans;
using Application.Features.Product.Validators;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest
    {
        public CreateProductCommand(string name, string description, double price, List<Ingredients> ingredients, double quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Ingredients = ingredients;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateProductCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException(ProductConstants.ProductCreateError, validationResult.ToDictionary());
                }

                var product = ProductAggregate.Create(request.Name, request.Description, request.Price, request.Ingredients, request.Quantity);

                await _productRepository.AddAsync(product, cancellationToken);
            }
        }
    }
}
