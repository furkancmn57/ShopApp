using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.User.Commands.Validators;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<UserAggregate>
    {
        public CreateUserCommand(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, UserAggregate>
        {
            private readonly IShopAppDbContext _context;

            public Handler(IShopAppDbContext context)
            {
                _context = context;
            }

            public async Task<UserAggregate> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateUserCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException("Kullanıcı eklerken hata oluştu.", validationResult.ToDictionary());
                }

                var userExist = await _context.Users.AnyAsync(x => x.Email == request.Email,cancellationToken);

                if (userExist)
                {
                    throw new BusinessException("Bu email adresi ile kayıtlı kullanıcı mevcut.");
                }

                var user = UserAggregate.Create(request.FirstName, request.LastName, request.Email);

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return user;
            }
        }
    }
}
