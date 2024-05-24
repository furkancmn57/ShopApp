using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressAggregate>
    {
        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetAddressByIdQuery, AddressAggregate>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<AddressAggregate> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
            {
                var address = await _context.Addresses.FindAsync(request.Id, cancellationToken);

                if (address is null)
                {
                    throw new NotFoundExcepiton("Adres Bulunamadı.");
                }

                return address;
            }
        }
    }
}
