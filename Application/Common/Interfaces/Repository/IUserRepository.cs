using Application.Common.Pagination;
using Application.Features.User.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<UserAggregate> GetByIdAsync(int id, CancellationToken token);
        Task<Pagination<GetUserResponse>> GetAsync(int page, int pageSize, CancellationToken token);
        Task<bool> UserExist(string email, CancellationToken token);
        Task AddAsync(UserAggregate user, CancellationToken token);
        Task UpdateAsync(UserAggregate user, CancellationToken token);
        Task DeleteAsync(UserAggregate user, CancellationToken token);
    }
}
