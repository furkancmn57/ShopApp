using Application.Common.Interfaces;
using Application.Features.Address.Constans;
using Application.Features.Address.Models;
using Application.Features.Address.Queries;
using Application.Features.User.Constants;
using Application.Features.User.Models;
using Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Models.Address.Request;
using WebApi.Models.User.Request;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisClient;

        public UserController(IMediator mediator, IRedisDbContext redisClient)
        {
            _mediator = mediator;
            _redisClient = redisClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken token, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var cacheKey = $"users_{page}_{pageSize}";

            var cacheValue = await _redisClient.Get<List<GetUserResponse>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetUserQuery
            {
                Page = page,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query, token);

            await _redisClient.Add(cacheKey, result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id, CancellationToken token)
        {
            var cacheKey = $"user_{id}";

            var cacheValue = await _redisClient.Get<GetUserResponse>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query, token);

            var response = new GetUserResponse
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                CreatedDate = result.CreatedDate,
                Addresses = result.Addresses.Select(a => new GetAddressResponse
                {
                    Id = a.Id,
                    AddressTitle = a.AddressTitle,
                    Address = a.Address,
                    CreatedDate = a.CreatedDate
                }).ToList()
            };

            await _redisClient.Add(cacheKey, response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            return Ok(UserConstants.UserUpdateSuccess);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, RemoveUserRequest request, CancellationToken token)
        {
            var cacheKey = $"user_{id}";

            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            await _redisClient.Delete(cacheKey);

            return Ok(UserConstants.UserDeleteSuccess);
        }

        // Address route

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{id}/addresses")]
        public async Task<IActionResult> CreateAddress([FromRoute] int id, [FromBody] CreateAddressRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            return Ok(AddressConstants.AddressAddSuccess);
        }
    }
}
