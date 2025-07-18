
using System.Net;
using BookBarter.API.Common.Models;
using BookBarter.Domain.Exceptions;
using FluentValidation;

namespace OnlineBookShop.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An exception has occured");

                switch (ex)
                {
                    case EntityNotFoundException _:
                    case ValidationException _:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await CreateExceptionResponseAsync(context, ex);
            }
        }

        private static Task CreateExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails
            {
                Messages = ex switch
                    {
                        ValidationException valEx => valEx.Errors.Select(e => e.ErrorMessage).ToList(),
                        _ => [ex.Message]
                    },
            }.ToString());
        }
    }
}