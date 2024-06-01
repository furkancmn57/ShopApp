using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Common.Pagination;
using Application.Features.Address.Models;
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
    public class GetAddressQuery : IRequest<Pagination<GetAddressResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public class Handler : IRequestHandler<GetAddressQuery, Pagination<GetAddressResponse>>
        {
            private readonly IAddressRepository _addressRepository;
            public Handler(IAddressRepository addressRepository)
            {
                _addressRepository = addressRepository;
            }

            public async Task<Pagination<GetAddressResponse>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
            {
                var addresses = await _addressRepository.GetAsync(request.Page, request.PageSize, cancellationToken);

                return addresses;
            }
        }
    }
}
