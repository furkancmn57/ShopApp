using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Tools;
using Application.Features.User.Constants;
using Application.Features.User.Validators;
using Application.Services.AuthService;
using Application.Services.PasswordService;
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
    public class CreateUserCommand : IRequest<AccessToken>
    {
        public CreateUserCommand(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, AccessToken>
        {
            private readonly IShopAppDbContext _context;
            private readonly IAuthService _authService;
            private readonly IPasswordService _passwordService;
            private readonly IRedisDbContext _redisClient;

            public Handler(IShopAppDbContext context, IAuthService authService, IPasswordService passwordService, IRedisDbContext redisClient)
            {
                _context = context;
                _authService = authService;
                _passwordService = passwordService;
               _redisClient = redisClient;
            }

            public async Task<AccessToken> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateUserCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException(UserConstants.UserCreateError, validationResult.ToDictionary());
                }

                var userExist = await _context.Users.AnyAsync(x => x.Email == request.Email,cancellationToken);

                if (userExist)
                {
                    throw new BusinessException(UserConstants.UserExist);
                }

                var hashPassword = _passwordService.HashPassword(request.Password);
                

                var user = UserAggregate.Create(request.FirstName, request.LastName, request.Email, hashPassword);

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var accessToken = await _authService.CreateAccessToken(user);

                var name = $"session_user_id_{user.Id}";
                var time = accessToken.Expiration - DateTime.Now;
                await _redisClient.AddString(name, accessToken.Token.ToString(), time);

                return accessToken;
            }
        }
    } 
}
