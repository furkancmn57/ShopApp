using Application.Features.Product.Commands;
using Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Product.Request;
using WebApi.Models.Product.Response;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken token)
        {
            var query = new GetProductQuery();
            var result = await _mediator.Send(query, token);

            var response = result.Select(x => new GetProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Ingredients = x.Ingredients,
                Quantity = x.Quantity
            }).ToList();


            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id, CancellationToken token)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query, token);

            var response = new GetProductResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                Ingredients = result.Ingredients,
                Quantity = result.Quantity
            };

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
            var query = request.ToCommand(id);
            await _mediator.Send(query, token);

            return Ok("Ürün Başarıyla Silindi.");
        }
    }
}
