using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly IConfiguration _configration;

        public AuthManager(IConfiguration configration)
        {
            _configration = configration;
        }

        public async Task<AccessToken> CreateAccessToken(UserAggregate user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("user_id", user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configration["Jwt:Issuer"],
                audience: _configration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToInt32(_configration["Jwt:Expiration"])),
                signingCredentials: credentials
            );

            var accessToken = new AccessToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return await Task.FromResult(accessToken);
        }
    }
}
