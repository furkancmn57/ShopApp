using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            CurrencyService currencyService = new CurrencyService();

            var currencies = await currencyService.GetCurrency();

            return Ok(currencies);
        }
    }
}
