using Application.Common.Interfaces;
using Application.Features.Address.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middlewares;
using WebApi.Models.Address.Request;
using WebApi.Models.Address.Response;

namespace WebApi.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisClient;

        public AddressController(IMediator mediator, IRedisDbContext redisClient)
        {
            _mediator = mediator;
            _redisClient = redisClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAddresses(CancellationToken token)
        {
            var cacheKey = "addresses";

            var cacheValue = await _redisClient.Get<List<GetAddressResponse>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetAddressQuery();
            var result = await _mediator.Send(query, token);

            var response = result.Select(x => new GetAddressResponse
            {
                Id = x.Id,
                AddressTitle = x.AddressTitle,
                Address = x.Address,
                CreatedDate = x.CreatedDate
            }).ToList();

            await _redisClient.Add(cacheKey, response);

            return Ok(response);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id, CancellationToken token)
        {
            var cacheKey = $"address_{id}";

            var cacheValue = await _redisClient.Get<GetAddressResponse>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetAddressByIdQuery(id);
            var result = await _mediator.Send(query, token);

            var response = new GetAddressResponse
            {
                Id = result.Id,
                AddressTitle = result.AddressTitle,
                Address = result.Address,
                CreatedDate = result.CreatedDate
            };

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, UpdateAddressRequest request, CancellationToken token)
        {
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            return Ok("Adres Başarıyla Güncellendi.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id, RemoveAddressRequest request, CancellationToken token)
        {
            var cacheKey = "addresses";
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            await _redisClient.Delete(cacheKey);

            return Ok("Adres Başarıyla Silindi.");
        }
    }
}
