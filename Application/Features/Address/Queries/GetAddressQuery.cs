using Application.Common.Interfaces;
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
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<Pagination<GetAddressResponse>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
            {
                var totaladdresses = await _context.Addresses.CountAsync(cancellationToken);

                var addresses = await _context.Addresses
                    .Select(x => new GetAddressResponse
                    {
                        Id = x.Id,
                        AddressTitle = x.AddressTitle,
                        Address = x.Address,
                        CreatedDate = x.CreatedDate
                    })
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                return new Pagination<GetAddressResponse>
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalCount = totaladdresses,
                    TotalPages = (int)Math.Ceiling(totaladdresses / (decimal)request.PageSize),
                    Data = addresses
                };
            }
        }
    }
}
