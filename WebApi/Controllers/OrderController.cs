using Application.Features.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Order.Request;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id, CancellationToken token)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query,token);

            return Ok(order);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken token)
        {
            var query = new GetOrderQuery();
            var orders = await _mediator.Send(query,token);

            return Ok(orders);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken token)
        {

            var command = request.ToCommand();
            await _mediator.Send(command);
            return Ok("Sipariş başarıyla oluşturuldu.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id,RemoveOrderRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);
            await _mediator.Send(command);
            return Ok("Sipariş başarıyla silindi.");
        }
    }
}
