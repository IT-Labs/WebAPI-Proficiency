using Microsoft.AspNetCore.Builder;
using WebApplication.Middlewares;

namespace WebApplication.MiddlewareExtensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseLogRequestsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestsMiddleware>();
        }

        public static IApplicationBuilder UseRefreshTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RefreshTokenMiddleware>();
        }

        public static IApplicationBuilder UseRequestCultureMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
