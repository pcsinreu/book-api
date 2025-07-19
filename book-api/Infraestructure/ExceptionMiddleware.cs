using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace book_api.Infraestructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logger = _serviceProvider.GetRequiredService<ILoggerService>();
                logger.Log($"Unhandled exception: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}