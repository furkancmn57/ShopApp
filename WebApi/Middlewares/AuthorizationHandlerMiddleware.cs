using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class AuthorizationHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IRedisDbContext _redisClient;

        public AuthorizationHandlerMiddleware(RequestDelegate next, IRedisDbContext redisClient)
        {
            _next = next;
            _redisClient = redisClient;
        }

        public async Task Invoke(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            if (authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.First(claim => claim.Type == "user_id").Value;
                // Redis üzerinden token'ı doğrula
                if (await ValidateTokenFromRedis(userId))
                {
                    await _next(context);
                }
            }

            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var response = new { success = false, message = "Buraya erişme yetkiniz yok." };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }


        private async Task<bool> ValidateTokenFromRedis(string userId)
        {
            var cacheKey = $"session_user_id_{userId}";

            var cacheValue = await _redisClient.KeyExist(cacheKey);

            return cacheValue;
        }
    }
}
