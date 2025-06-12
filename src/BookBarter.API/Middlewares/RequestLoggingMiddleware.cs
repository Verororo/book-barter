using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace BookBarter.API.Middleware;

public class RequestLoggingOptions
{
    public List<string> SensitiveHeaders { get; set; } = new()
    {
        "Authorization", "Cookie", "Set-Cookie", "X-API-Key", "X-Auth-Token"
    };
}

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly RequestLoggingOptions _options;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger,
        IOptions<RequestLoggingOptions> options)
    {
        _next = next;
        _logger = logger;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = context.TraceIdentifier;
        var request = context.Request;

        var requestInfo = CaptureRequestInfo(request, requestId);

        _logger.LogInformation($"Request started: {requestInfo}");

        try
        {
            await _next(context);
        }
        finally {
            stopwatch.Stop();

            var response = context.Response;
            var responseInfo = CaptureResponseInfo(response, requestId, stopwatch.ElapsedMilliseconds);

            _logger.LogInformation($"Request completed: {responseInfo}");
        }
    }

    // create new type instead of using object
    private object CaptureRequestInfo(HttpRequest request, string requestId)
    {
        var requestInfo = new
        {
            RequestId = requestId,
            Method = request.Method,
            Path = request.Path.Value,
            Protocol = request.Protocol,
            ContentType = request.ContentType,
            Headers = JsonSerializer.Serialize(GetHeaders(request.Headers))
        };

        return requestInfo;
    }

    private object CaptureResponseInfo(HttpResponse response, string requestId, long durationMs)
    {
        var responseInfo = new
        {
            RequestId = requestId,
            StatusCode = response.StatusCode,
            ContentType = response.ContentType,
            Headers = JsonSerializer.Serialize(GetHeaders(response.Headers)),
            Duration = durationMs
        };

        return responseInfo;
    }

    private Dictionary<string, string> GetHeaders(IHeaderDictionary headers)
    {
        return headers
            .Where(h => !_options.SensitiveHeaders.Contains(h.Key, StringComparer.OrdinalIgnoreCase))
            .ToDictionary(
                h => h.Key,
                h => string.Join(", ", (string)h.Value!));
    }
}
