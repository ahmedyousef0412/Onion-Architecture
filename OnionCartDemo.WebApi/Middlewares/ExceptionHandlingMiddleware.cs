using OnionCartDemo.Domain.Exceptions;
using OnionCartDemo.Application.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnionCartDemo.WebApi.Middlewares;

public  class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandlingExceptionAsync(context, ex);
        }
    }

    private static async Task HandlingExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            DomainException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            status = context.Response.StatusCode,
            error = ex.GetType().Name,
            message = ex.Message,
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
