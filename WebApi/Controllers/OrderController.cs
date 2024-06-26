﻿using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Features.Order.Constants;
using Application.Features.Order.Models;
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

            await _redisClient.Add(cacheKey, order);

            return Ok(order);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken token)
        {
            page = page.HasValue ? page.Value : 1;
            pageSize = pageSize.HasValue ? pageSize.Value : 10;

            string cacheKey = $"orders_{page}_{pageSize}";

            var cacheValue = await _redisClient.Get<Pagination<GetOrderByIdResponse>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }


            var query = new GetOrderQuery(page.Value,pageSize.Value);
            var orders = await _mediator.Send(query,token);
           

            await _redisClient.Add(cacheKey, orders);

            return Ok(orders);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken token)
        {

            var command = request.ToCommand();
            await _mediator.Send(command,token);
            return Ok(OrderConstants.OrderAddSuccess);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request, CancellationToken token)
        {
            var command = request.ToCommand(id);

            await _mediator.Send(command,token);

            return Ok(OrderConstants.OrderUpdate);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id,RemoveOrderRequest request, CancellationToken token)
        {
            string cacheKey = $"order_{id}";

            var command = request.ToCommand(id);
            await _mediator.Send(command,token);
            await _redisClient.Delete(cacheKey);

            return Ok(OrderConstants.OrderDeleteSuccess);
        }
    }
}
