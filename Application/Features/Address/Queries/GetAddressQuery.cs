using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Queries
{
    public class GetAddressQuery : IRequest<List<AddressAggregate>>
    {
        public class Handler : IRequestHandler<GetAddressQuery, List<AddressAggregate>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<AddressAggregate>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
            {
                var addresses = await _context.Addresses.ToListAsync(cancellationToken);

                return addresses;
            }
        }
    }
}
