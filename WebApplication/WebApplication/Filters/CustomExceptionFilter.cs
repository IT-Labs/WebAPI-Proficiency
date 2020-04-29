using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebApplication.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly Serilog.ILogger _logger;
        public CustomExceptionFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var message = context.Exception.Message;

            context.ExceptionHandled = true;

            _logger.Information("This is handled by custom exception filter");
            _logger.Error(default, context.Exception, context.Exception.Message);

            var response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            response.WriteAsync(new
            {
                ErrorCode = (int)status,
                ErrorMessage = message,
                ErrorDescription = "This is handled by custom exception filter"
            }.ToString());
        }
    }
}
