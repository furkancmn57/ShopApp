using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.Address.Models;
using Application.Features.User.Constants;
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
    public class GetUserByIdQuery : IRequest<GetUserResponse>
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public class Handler : IRequestHandler<GetUserByIdQuery, GetUserResponse>
        {
            private readonly IUserRepository _userRepository;
            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<GetUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

                var response = new GetUserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedDate = user.CreatedDate,
                    Addresses = user.Addresses.Select(a => new GetAddressResponse
                    {
                        Id = a.Id,
                        AddressTitle = a.AddressTitle,
                        Address = a.Address,
                        CreatedDate = a.CreatedDate
                    }).ToList()
                };

                return response;
            }
        }
    }
}
