using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Pagination;
using Application.Features.Order.Models;
using Application.Features.Product.Constans;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IShopAppDbContext _context;

        public OrderRepository(IShopAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(OrderAggregate order, CancellationToken token)
        {
            await _context.Orders.AddAsync(order, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(OrderAggregate order, CancellationToken token)
        {

            order.IsDeleted = true;
            order.DeletedDate = DateTime.Now;

            _context.Orders.UpdateRange(order);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Pagination<GetOrdersResponse>> GetAsync(int page, int pageSize, CancellationToken token)
        {
            var totalorders = await _context.Orders
                .Where(x=> !x.IsDeleted)
                .CountAsync(token);

            var orders = await _context.Orders
                .Where(i => i.IsDeleted == false)
                .Select(x => new GetOrdersResponse
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    CustomerName = x.CustomerName,
                    DiscountAmount = x.DiscountAmount,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status.ToString(),
                    CreatedDate = x.CreatedDate,
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            var result = new Pagination<GetOrdersResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalorders,
                TotalPages = (int)Math.Ceiling(totalorders / (decimal)pageSize),
                Data = orders
            };

            return result;
        }

        public async Task<OrderAggregate> GetByIdAsync(int id, CancellationToken token)
        {
            var order = await _context.Orders
                .Include(i => i.Products)
                .Include(i => i.Address)
                .Include(i => i.User)
                .Where(i => !i.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id, token);

            if (order is null)
            {
                throw new NotFoundExcepiton(ProductConstants.ProductNotFound);
            }

            return order;
        }

        public async Task UpdateAsync(OrderAggregate order, CancellationToken token)
        {
            order.Update(order.Status);
            await _context.SaveChangesAsync(token);
        }
    }
}
