using Application.Common.Exceptions;
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
    public class GetUserByIdQuery : IRequest<UserAggregate>
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetUserByIdQuery, UserAggregate>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<UserAggregate> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(u => u.Addresses)
                    .Include(u => u.Orders)
                    .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                
                if (user is null)
                {
                    throw new NotFoundExcepiton("Kullanıcı Bulunamadı.");
                }

                return user;
            }
        }
    }
}
