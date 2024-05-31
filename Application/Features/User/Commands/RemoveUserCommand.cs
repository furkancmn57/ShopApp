using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.User.Constants;
using MediatR;
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
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(RemoveUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id);

                if (user is null)
                {
                    throw new NotFoundExcepiton(UserConstants.UserNotFound);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
