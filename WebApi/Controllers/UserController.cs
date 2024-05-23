using Application.Features.Address.Queries;
using Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Address.Request;
using WebApi.Models.User.Reponse;
using WebApi.Models.User.Request;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken token)
        {
            var query = new GetUserQuery();
            var result = await _mediator.Send(query, token);


            return Ok(result);
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
        public async Task<IActionResult> DeleteUser([FromRoute] int id,RemoveUserRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command, token);

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
