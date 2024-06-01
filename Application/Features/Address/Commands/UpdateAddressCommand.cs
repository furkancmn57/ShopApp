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
            private readonly IAddressRepository _addressRepository;

            public Handler(IAddressRepository addressRepository)
            {
                _addressRepository = addressRepository;
            }

            public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

                await _addressRepository.UpdateAsync(address, cancellationToken);
            }
        }
    }
}
