using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.User.Commands.Validators;
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
    public class LoginUserCommand : IRequest<AccessToken>
    {
        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginUserCommand, AccessToken>
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

            public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

                var validator = new LoginUserCommandValidator();
                var validationResult = validator.Validate(request);

                if (validationResult.IsValid == false)
                {
                    throw new ValidationException("Giriş yaparken bir hata oluştu.", validationResult.ToDictionary());
                }

                if (user is null)
                {
                    throw new BusinessException("Email veya şifre hatalı.");
                }

                var passwordCheck = _passwordService.VerifyPassword(request.Password, user.Password);

                if (!passwordCheck)
                {
                    throw new BusinessException("Email veya şifre hatalı.");
                }


                var accessToken = await _authService.CreateAccessToken(user);
                var name = $"session_user_id_{user.Id}";
                var time = accessToken.Expiration - DateTime.Now;
                await _redisClient.AddString(name, accessToken.Token.ToString(), time);

                return accessToken;
            }
        }
    }
}
