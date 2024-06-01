using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repository;
using Application.Features.User.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public class RemoveUserCommand : IRequest
    {
        public RemoveUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public class Handler : IRequestHandler<RemoveUserCommand>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

                await _userRepository.DeleteAsync(user, cancellationToken);
            }
        }
    }
}
