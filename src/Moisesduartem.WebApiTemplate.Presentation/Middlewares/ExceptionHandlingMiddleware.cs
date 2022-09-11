using Moisesduartem.WebApiTemplate.Application.V1.Shared;
using System.Net;

namespace Moisesduartem.WebApiTemplate.Presentation.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Failed to process the request");
                await WriteErrorResponse(context);
            }
        }

        private Task WriteErrorResponse(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var result = Result<object>.Create().Error("UnexpectedError", "Failed to process the request");

            return response.WriteAsJsonAsync(result);
        }
    }
}
