using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Address.Constans;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Commands
{
    public class UpdateAddressCommand : IRequest
    {
        public UpdateAddressCommand(int id, string address, string addressTitle)
        {
            Id = id;
            Address = address;
            AddressTitle = addressTitle;
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string AddressTitle { get; set; }

        public class Handler : IRequestHandler<UpdateAddressCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                
                var address = await _context.Addresses.FindAsync(request.Id);

                if (address is null)
                {
                    throw new NotFoundExcepiton(AddressConstants.AddressNotFound);
                }

                address.Update(request.AddressTitle, request.Address);
                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
