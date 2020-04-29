using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication.Contracts;

namespace WebApplication.Filters
{
    public class ResponseFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var content = context.Result as ObjectResult;

            if (content?.Value is IResponse)
            {
                context.HttpContext.Response.StatusCode = (int)((IResponse)content.Value).Status;
            }
        }

    }
}
