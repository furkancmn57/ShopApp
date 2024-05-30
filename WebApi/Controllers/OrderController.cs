using Application.Common.Interfaces;
using Application.Features.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Address.Response;
using WebApi.Models.Order.Request;
using WebApi.Models.Order.Response;
using WebApi.Models.Product.Response;
using WebApi.Models.User.Reponse;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisClient;

        public OrderController(IMediator mediator, IRedisDbContext redisClient)
        {
            _mediator = mediator;
            _redisClient = redisClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id, CancellationToken token)
        {
            string cacheKey = $"order_{id}";

            var cacheValue = await _redisClient.Get<GetOrderByIdResponse>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query, token);

            var response = new GetOrderByIdResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                DiscountAmount = order.DiscountAmount,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Products = order.Products.Select(p => new GetProductResponseWithOrder
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToList(),
                Address = new GetAddressResponseWithOrder
                {
                    Address = order.Address.Address
                },
                User = new GetUserResponseWithOrder
                {
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    Email = order.User.Email
                },
                OrderDate = order.OrderDate,
            };

            await _redisClient.Add(cacheKey, response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken token)
        {
            string cacheKey = "orders";

            var cacheValue = await _redisClient.Get<GetOrderByIdResponse>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetOrderQuery();
            var orders = await _mediator.Send(query,token);
            
            var response = orders.Select(x => new GetOrdersResponse
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                CustomerName = x.CustomerName,
                DiscountAmount = x.DiscountAmount,
                TotalAmount = x.TotalAmount,
                Status = x.Status.ToString(),
                //User = new GetUserResponseWithOrder
                //{
                //    FirstName = x.User.FirstName,
                //    LastName = x.User.LastName,
                //    Email = x.User.Email
                //},
                OrderDate = x.OrderDate
            }).ToList();


            await _redisClient.Add(cacheKey, orders);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken token)
        {

            var command = request.ToCommand();
            await _mediator.Send(command,token);
            return Ok("Sipariş başarıyla oluşturuldu.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);

            await _mediator.Send(command,token);

            return Ok("Sipariş başarıyla güncellendi.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id,RemoveOrderRequest request, CancellationToken token)
        {
            string cacheKey = $"order_{id}";

            var command = request.ToCommand(id);

            await _mediator.Send(command,token);
            await _redisClient.Delete(cacheKey);

            return Ok("Sipariş başarıyla silindi.");
        }
    }
}
