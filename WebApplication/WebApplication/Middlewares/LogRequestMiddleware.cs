using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class LogRequestsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogRequestsMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            var url = context.Request.GetDisplayUrl();
            var request = FlattenRequest(context.Request, url);

            var original = context.Response.Body;
            var stream = new MemoryStream();
            context.Response.Body = stream;

            await _next(context);

            stream.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(stream).ReadToEnd();
            var response = FlattenResponse(context.Response, body);
            stream.Seek(0, SeekOrigin.Begin);

            _logger.Information($"It took {watch.ElapsedMilliseconds} milliseconds for {request} {response}");

            await stream.CopyToAsync(original);

        }

        string FlattenRequest(HttpRequest request, string url)
        {
            return $"Request url: {url} method: {request.Method}, query string: {request.QueryString}. ";
        }
        string FlattenResponse(HttpResponse response, string body)
        {
            var headers = response.Headers.Keys.Aggregate(string.Empty, (current, key) => current + $"{key}: {response.Headers[key]}; ");
            return $"Response headers: {headers}, body: {body}. ";
        }
    }
}
