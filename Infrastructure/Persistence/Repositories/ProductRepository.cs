using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Pagination;
using Application.Features.Product.Constans;
using Application.Features.Product.Models;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IShopAppDbContext _context;
        public ProductRepository(IShopAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductAggregate product, CancellationToken token)
        {
            await _context.Products.AddAsync(product, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(ProductAggregate product, CancellationToken token)
        {
            product.IsDeleted = true;
            product.DeletedDate = DateTime.Now;

            _context.Products.UpdateRange(product);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Pagination<GetProductResponse>> GetAsync(int page, int pageSize, CancellationToken token)
        {
            var totalproducts = await _context.Products.CountAsync(token);

            var products = await _context.Products
                .Where(i => i.IsDeleted == false)
                .Select(x => new GetProductResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Ingredients = x.Ingredients,
                    CreatedDate = x.CreatedDate
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);


            var result = new Pagination<GetProductResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalproducts,
                TotalPages = (int)Math.Ceiling(totalproducts / (decimal)pageSize),
                Data = products
            };

            return result;
        }

        public async Task<ProductAggregate> GetByIdAsync(int id, CancellationToken token)
        {
            var product = await _context.Products
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id, token);

            if (product is null)
            {
                throw new NotFoundExcepiton(ProductConstants.ProductNotFound);
            }

            return product;
        }

        public async Task UpdateAsync(ProductAggregate product, CancellationToken token)
        {
            product.Update(product.Name, product.Description, product.Price, product.Ingredients, product.Quantity);
            await _context.SaveChangesAsync(token);
        }
    }
}
