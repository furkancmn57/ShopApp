using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Pagination;
using Application.Features.Address.Models;
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
    public class AddressRepository : IAddressRepository
    {
        private readonly IShopAppDbContext _context;

        public AddressRepository(IShopAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AddressAggregate address, CancellationToken token)
        {
            await _context.Addresses.AddAsync(address, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(AddressAggregate address, CancellationToken token)
        {
            address.IsDeleted = true;
            address.DeletedDate = DateTime.Now;

            _context.Addresses.UpdateRange(address);
            await _context.SaveChangesAsync(token);
        }

        public async Task<AddressAggregate> GetByIdAsync(int id, CancellationToken token)
        {
            var address = await _context.Addresses
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id, token);

            if (address is null)
            {
                throw new NotFoundExcepiton(ProductConstants.ProductNotFound);
            }

            return address;
        }

        public async Task<Pagination<GetAddressResponse>> GetAsync(int page, int pageSize, CancellationToken token)
        {
            var totaladdresses = await _context.Addresses
                .Where(x => x.IsDeleted == false)
                .CountAsync(token);

            var addresses = await _context.Addresses
                .Where(i => i.IsDeleted == false)
                .Select(x => new GetAddressResponse
                {
                    Id = x.Id,
                    AddressTitle = x.AddressTitle,
                    Address = x.Address,
                    CreatedDate = x.CreatedDate
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            var result = new Pagination<GetAddressResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totaladdresses,
                TotalPages = (int)Math.Ceiling(totaladdresses / (decimal)pageSize),
                Data = addresses
            };

            return result;
        }

        public async Task UpdateAsync(AddressAggregate address, CancellationToken token)
        {

            address.Update(address.AddressTitle, address.Address);
            await _context.SaveChangesAsync(token);
        }
    }
}
