using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.Features.Product.Commands;
using Application.Features.Product.Models;
using Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Product.Request;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisDbContext _redisClient;

        public ProductController(IMediator mediator, IRedisDbContext redisClient)
        {
            _mediator = mediator;
            _redisClient = redisClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken token, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var cacheKey = $"products_{page}_{pageSize}";

            var cacheValue = await _redisClient.Get<Pagination<GetProductResponse>>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetProductQuery
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
        public async Task<IActionResult> GetProductById([FromRoute] int id, CancellationToken token)
        {
            var cacheKey = $"product_{id}";

            var cacheValue = await _redisClient.Get<GetProductResponse>(cacheKey);

            if (cacheValue is not null)
            {
                return Ok(cacheValue);
            }

            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query, token);

            var response = new GetProductResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                Ingredients = result.Ingredients,
                Quantity = result.Quantity,
                CreatedDate = result.CreatedDate
            };

            await _redisClient.Add(cacheKey, response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok("Ürün Başarıyla Oluşturuldu.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, UpdateProductRequest request, CancellationToken token)
        {
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            return Ok("Ürün Başarıyla Güncellendi.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id, RemoveProuctRequest request, CancellationToken token)
        {
            var cacheKey = $"product_{id}";

            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            await _redisClient.Delete(cacheKey);

            return Ok("Ürün Başarıyla Silindi.");
        }
    }
}
