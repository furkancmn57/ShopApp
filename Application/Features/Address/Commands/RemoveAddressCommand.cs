using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Commands
{
    public class RemoveAddressCommand : IRequest
    {
        public RemoveAddressCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class Handler : IRequestHandler<RemoveAddressCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
            {
                var address = await _context.Addresses.FindAsync(request.Id);

                if (address is null)
                {
                    throw new BusinessException("Address Bulunamadı.", 404);
                }

                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
