using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Pagination;
using Application.Features.Address.Models;
using Application.Features.User.Constants;
using Application.Features.User.Models;
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
    public class UserRepository : IUserRepository
    {
        private readonly IShopAppDbContext _context;
        public UserRepository(IShopAppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserAggregate user, CancellationToken token)
        {
            await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(UserAggregate user, CancellationToken token)
        {
            user.IsDeleted = true;
            user.DeletedDate = DateTime.Now;

            _context.Users.UpdateRange(user);
            await _context.SaveChangesAsync(token);
        }

        public async Task<Pagination<GetUserResponse>> GetAsync(int page, int pageSize, CancellationToken token)
        {
            var totalusers = await _context.Users
                .Where(x => x.IsDeleted == false)
                .CountAsync(token);

            var users = await _context.Users
                .Where(x => x.IsDeleted == false)
                .Include(i => i.Addresses)
                .Select(x => new GetUserResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    CreatedDate = x.CreatedDate,
                    Addresses = x.Addresses
                    .Where(x => x.IsDeleted == false)
                    .Select(a => new GetAddressResponse
                    {
                        Id = a.Id,
                        AddressTitle = a.AddressTitle,
                        Address = a.Address,
                        CreatedDate = a.CreatedDate
                    }).ToList()
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            var result = new Pagination<GetUserResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalusers,
                TotalPages = (int)Math.Ceiling(totalusers / (decimal)pageSize),
                Data = users
            };


            return result;
        }

        public async Task<UserAggregate> GetByIdAsync(int id, CancellationToken token)
        {
            var user = await _context.Users
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == id, token);

            if (user is null)
            {
                throw new NotFoundExcepiton(UserConstants.UserNotFound);
            }

            return user;
        }

        public async Task UpdateAsync(UserAggregate user, CancellationToken token)
        {
            user.Update(user.FirstName, user.LastName, user.Email);
            await _context.SaveChangesAsync(token);
        }

        public async Task<bool> UserExist(string email, CancellationToken token)
        {
            var exist = await _context.Users
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Email == email, token);

            return exist is not null;
        }
    }
}
