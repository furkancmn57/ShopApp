using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RTools_NTS.Util;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Models.User.Request;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisClient;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command, token);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command, token);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            return Ok("Çıkış işlemi başarılı.");
        }
    }
}
