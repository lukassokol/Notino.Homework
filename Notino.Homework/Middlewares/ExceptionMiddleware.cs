using Notino.Homework.Middlewares.Models;
using System.Net;

namespace Notino.Homework.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException)
        {
            await HandleArgumentExceptionAsync(context);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(context);
        }
        // TODO: Add more exceptions for better response
    }

    private async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(
            new ErrorResponse(context.Response.StatusCode, "Internal Server Error").ToString());
    }

    private async Task HandleArgumentExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        await context.Response.WriteAsync(
            new ErrorResponse(context.Response.StatusCode, "Cannot process the request because it is malformed or incorrect.").ToString());
    }
}
