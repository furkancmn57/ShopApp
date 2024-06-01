using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Features.Address.Constans;
using Application.Features.Address.Models;
using Application.Features.Address.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Middlewares;
using WebApi.Models.Address.Request;

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
        public async Task<IActionResult> GetAddresses(CancellationToken token, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var cacheKey = $"addresses_{page}_{pageSize}";

            var cacheValue = await _redisClient.Get<Pagination<GetAddressResponse>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetAddressQuery
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

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, UpdateAddressRequest request, CancellationToken token)
        {
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            return Ok(AddressConstants.AddressUpdateSuccess);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id, RemoveAddressRequest request, CancellationToken token)
        {
            var cacheKey = "addresses";
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            await _redisClient.Delete(cacheKey);

            return Ok(AddressConstants.AddressDeleteSuccess);
        }
    }
}
