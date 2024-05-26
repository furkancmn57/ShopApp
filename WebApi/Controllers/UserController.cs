using Application.Common.Interfaces;
using Application.Features.Address.Queries;
using Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Address.Request;
using WebApi.Models.Address.Response;
using WebApi.Models.User.Reponse;
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
        public async Task<IActionResult> GetUsers(CancellationToken token)
        {
            var cacheKey = "users";

            var cacheValue = await _redisClient.Get<List<GetUserResponse>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetUserQuery();

            var result = await _mediator.Send(query, token);

            var response = result.Select(x => new GetUserResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Addresses = x.Addresses.Select(a => new GetAddressResponse
                {
                    Id = a.Id,
                    AddressTitle = a.AddressTitle,
                    Address = a.Address
                }).ToList()
            }).ToList();

            await _redisClient.Add(cacheKey, response);

            return Ok(response);
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
                Addresses = result.Addresses.Select(x => new GetAddressResponse
                {
                    Id = x.Id,
                    AddressTitle = x.AddressTitle,
                    Address = x.Address,
                }).ToList()
            };

            await _redisClient.Add(cacheKey, response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command, token);

            return Ok("Kullanıcı Başarıyla Oluşturuldu.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            return Ok("Kullanıcı Başarıyla Güncellendi.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, RemoveUserRequest request, CancellationToken token)
        {
            var cacheKey = $"user_{id}";

            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            await _redisClient.Delete(cacheKey);

            return Ok("Kullanıcı Başarıyla Silindi.");
        }

        // Address route

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{id}/addresses")]
        public async Task<IActionResult> CreateAddress([FromRoute] int id, [FromBody] CreateAddressRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

            return Ok("Adres Başarıyla Oluşturuldu.");
        }
    }
}
