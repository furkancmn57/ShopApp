using Application.Features.Address.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Address.Request;

namespace WebApi.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAddresses(CancellationToken token)
        {
            var query = new GetAddressQuery();
            var result = await _mediator.Send(query, token);

            return Ok(result);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id, CancellationToken token)
        {
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

            return Ok("Adres Başarıyla Güncellendi.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id, RemoveAddressRequest request, CancellationToken token)
        {
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            return Ok("Adres Başarıyla Silindi.");
        }
    }
}
