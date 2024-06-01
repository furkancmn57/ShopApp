using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
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
            private readonly IUserRepository _userRepository;
            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Pagination<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAsync(request.Page, request.PageSize, cancellationToken);

                return users;
            }
        }
    }
}
