using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Address.Constans;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IAddressRepository _addressRepository;

            public Handler(IShopAppDbContext context, IAddressRepository addressRepository)
            {
                _context = context;
                _addressRepository = addressRepository;
            }

            public async Task Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
            {
                var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

                await _addressRepository.DeleteAsync(address,cancellationToken);
            }
        }
    }
}
