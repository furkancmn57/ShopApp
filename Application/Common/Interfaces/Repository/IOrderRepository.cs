using Application.Common.Pagination;
using Application.Features.Order.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<OrderAggregate> GetByIdAsync(int id,CancellationToken token);
        Task<Pagination<GetOrdersResponse>> GetAsync(int page, int pageSize, CancellationToken token);
        Task CreateAsync(OrderAggregate order, CancellationToken token);
        Task UpdateAsync(OrderAggregate order, CancellationToken token);
        Task DeleteAsync(OrderAggregate order, CancellationToken token);
    }
}
