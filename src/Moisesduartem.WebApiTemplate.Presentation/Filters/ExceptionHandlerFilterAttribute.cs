using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moisesduartem.WebApiTemplate.Application.V1.Shared;
using System.Net;

namespace Moisesduartem.WebApiTemplate.Presentation.Filters
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

            var result = Result<object>.Create().Error("UnexpectedError", "Failed to process the request");

            context.Result = new ObjectResult(result)
            {
                StatusCode = (int) HttpStatusCode.InternalServerError
            };
        }
    }
}
