using BookBarter.API.Middleware;
using BookBarter.API.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineBookShop.API.Middlewares;

namespace BookBarter.API.Extensions;

public static class RequestTimingMiddlewareExtensions
{
    public static IApplicationBuilder UseTransaction(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TransactionMiddleware>();
    }
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
