using Application.Common.Exceptions;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var response = new { success = false, message = ex.Message };
                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
            catch (NotFoundExcepiton ex)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";

                var response = new { success = false, message = ex.Message };
                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var response = new { success = false, message = ex.Message, errors = ex.Errors };
                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new { success = false, message = ex.Message };
                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
