using Microsoft.AspNetCore.Http;
using OnionCartDemo.Application.Exceptions;
using OnionCartDemo.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace OnionCartDemo.WebApi.Middlewares;

public  class ExceptionHandlingMiddleware(RequestDelegate next ,
    IHostEnvironment env,
    ILogger<ExceptionHandlingMiddleware> logger)
{

    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _env = env;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandlingExceptionAsync(context, exception);
        }
    }

    private  async Task HandlingExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, message, source) = MapException(exception);

        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var response = new
        {
            status = statusCode,
            title,
            message,
            source = _env.IsDevelopment() ? source : null
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(response);
    }
    private static (int StatusCode, string Title, string Message, string Source) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException => (
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "One or more validation errors occurred.",
                "Application"
            ),

            NotFoundException notFoundException => (
                StatusCodes.Status404NotFound,
                "NotFound",
                notFoundException.Message,
                "Application"
            ),

            DomainException domainException => (
                StatusCodes.Status400BadRequest,
                "DomainError",
                domainException.Message,
                "Domain"
            ),

            ApplicationException applicationException => (
                StatusCodes.Status400BadRequest,
                "ApplicationError",
                applicationException.Message,
                "Application"
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                "InternalServerError",
                "An unexpected error has occurred.",
                "Infrastructure"
            )
        };
    }
}
