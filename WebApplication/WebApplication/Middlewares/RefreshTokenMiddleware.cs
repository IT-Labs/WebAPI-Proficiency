using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Contracts;

namespace WebApplication
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
      //  private readonly IManager<Principal> _manager;

        public RefreshTokenMiddleware(RequestDelegate next
            //, IManager<Principal> manager
            )
        {
            _next = next;
          //  _manager = manager;
        }

        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headersRequest = context.Request.Headers;
            IHeaderDictionary headersResponse = context.Response.Headers;

            var token = headersRequest["Authorization"];

            await _next(context);

            if (!string.IsNullOrWhiteSpace(token))
            {
                if (CheckStatus(context.Response.StatusCode))
                {
                   // var result = _manager.RefreshIfActive<Principal>(token);
                    //if (result?.Status == HttpStatusCode.OK && result.Payload != null && token != result.Payload)
                    //{
                    //    headersResponse.Add("X-Authorization", result.Payload);
                    //}
                }
            }
        }

        private bool CheckStatus(int statusCode)
        {
            var code = (HttpStatusCode)statusCode;
            var statuses = new List<HttpStatusCode>
            {
                HttpStatusCode.OK,
                    HttpStatusCode.BadRequest,
                    HttpStatusCode.NotFound,
                    HttpStatusCode.InternalServerError,
                    HttpStatusCode.NotAcceptable
            };

            return statuses.Contains(code);
        }
    }
}
