using BookBarter.Infrastructure;

namespace BookBarter.API.Middlewares;

public class TransactionMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext)
    {
        if (httpContext.Request.Method == HttpMethod.Get.Method)
        {
            await _next(httpContext);
            return;
        }

        using (var transaction = await dbContext.Database.BeginTransactionAsync())
        {
            await _next(httpContext);

            await dbContext.Database.CommitTransactionAsync();
        }
    }
}
