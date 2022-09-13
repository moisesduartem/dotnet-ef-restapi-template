using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestApi.Application.V1.Shared;
using System.Net;

namespace RestApi.Filters
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandlerFilterAttribute> _logger;

        public ExceptionHandlerFilterAttribute(ILogger<ExceptionHandlerFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            _logger.LogCritical(context.Exception, "Failed to process the request");

            var result = Result.Create().Error("Failed to process the request");

            context.Result = new ObjectResult(result)
            {
                StatusCode = (int) HttpStatusCode.InternalServerError
            };
        }
    }
}
