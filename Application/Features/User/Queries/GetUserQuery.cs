using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public class GetUserQuery : IRequest<List<UserAggregate>>
    {
        public class Handler : IRequestHandler<GetUserQuery, List<UserAggregate>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<UserAggregate>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.Include(i => i.Addresses).Include(i => i.Orders).ToListAsync(cancellationToken);

                return users;
            }
        }
    }
}
