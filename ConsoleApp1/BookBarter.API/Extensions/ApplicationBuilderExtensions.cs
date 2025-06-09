using BookBarter.API.Middleware;
using Microsoft.Extensions.Options;

namespace BookBarter.API.Extensions;

public static class RequestTimingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
