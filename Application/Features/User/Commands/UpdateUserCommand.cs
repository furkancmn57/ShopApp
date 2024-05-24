using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserCommand(int id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id);

                if (user is null)
                {
                    throw new NotFoundExcepiton("User Bulunamadı.");
                }

                user.Update(request.FirstName, request.LastName, request.Email);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
