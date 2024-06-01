using Application.Common.Pagination;
using Application.Features.Address.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository
{
    public interface IAddressRepository
    {
        Task<AddressAggregate> GetByIdAsync(int id, CancellationToken token);
        Task<Pagination<GetAddressResponse>> GetAsync(int page, int pageSize, CancellationToken token);
        Task AddAsync(AddressAggregate address, CancellationToken token);
        Task UpdateAsync(AddressAggregate address, CancellationToken token);
        Task DeleteAsync(AddressAggregate address, CancellationToken token);
    }
}
