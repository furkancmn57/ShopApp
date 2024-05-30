using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Features.Address.Models;
using Application.Features.User.Models;
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
    public class GetUserQuery : IRequest<Pagination<GetUserResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public class Handler : IRequestHandler<GetUserQuery, Pagination<GetUserResponse>>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<Pagination<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var totalusers = await _context.Users.CountAsync(cancellationToken);

                var users = await _context.Users
                    .Include(i => i.Addresses)
                    .Select(x => new GetUserResponse
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        CreatedDate = x.CreatedDate,
                        Addresses = x.Addresses.Select(a => new GetAddressResponse
                        {
                            Id = a.Id,
                            AddressTitle = a.AddressTitle,
                            Address = a.Address,
                            CreatedDate = a.CreatedDate
                        }).ToList()
                    })
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);





                return new Pagination<GetUserResponse>
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalCount = totalusers,
                    TotalPages = (int)Math.Ceiling(totalusers / (decimal)request.PageSize),
                    Data = users
                };
            }
        }
    }
}
