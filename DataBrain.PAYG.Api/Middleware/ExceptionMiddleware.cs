using System.Net;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;

namespace DataBrain.PAYG.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Error(ex, "An unexpected error occurred");

                // Handle the exception and set the response status code
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var message = "An unexpected error occurred";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message }));
            }
        }
    }
}
