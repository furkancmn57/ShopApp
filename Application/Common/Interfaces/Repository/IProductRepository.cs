using Application.Common.Pagination;
using Application.Features.Product.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<ProductAggregate> GetByIdAsync(int id, CancellationToken token);
        Task<Pagination<GetProductResponse>> GetAsync(int page, int pageSize, CancellationToken token);
        Task AddAsync(ProductAggregate product, CancellationToken token);
        Task UpdateAsync(ProductAggregate product, CancellationToken token);
        Task DeleteAsync(ProductAggregate product, CancellationToken token);
    }
}
