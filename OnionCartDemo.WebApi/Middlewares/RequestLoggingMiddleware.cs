using System.Diagnostics;

namespace OnionCartDemo.WebApi.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

   
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        var stopWatch = Stopwatch.StartNew();

        _logger.LogInformation("Incoming {Method} request to {Path}", context.Request.Method, context.Request.Path);

        await _next(context);

        stopWatch.Stop();


        //Completed   GET    /api/products   with    status 200 in 551 ms
        _logger.LogInformation("Completed {Method} {Path} with status {StatusCode} in {Elapsed} ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopWatch.ElapsedMilliseconds);
    }
}
