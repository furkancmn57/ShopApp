using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Address.Constans;
using Application.Features.Address.Models;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Address.Queries
{
    public class GetAddressByIdQuery : IRequest<GetAddressResponse>
    {
        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetAddressByIdQuery, GetAddressResponse>
        {
            private readonly IAddressRepository _addressRepository;

            public Handler(IAddressRepository addressRepository)
            {
                _addressRepository = addressRepository;
            }

            public async Task<GetAddressResponse> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
            {
                var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

                var response = new GetAddressResponse
                {
                    Id = address.Id,
                    Address = address.Address,
                    AddressTitle = address.AddressTitle,
                    CreatedDate = address.CreatedDate,
                };

                return response;
            }
        }
    }
}
