using FluentValidation;
using TES.Domain.Common;

namespace TES.WebMVC.Extensions;

/// <summary>
/// Middleware para centralizar tratamento de exceções e retornar respostas padronizadas.
/// </summary>
public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception occurred");
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var acceptsHtml = context.Request.Headers["Accept"].ToString().Contains("text/html");

        if (acceptsHtml)
        {
            context.Response.Redirect($"/Home/Error?message={Uri.EscapeDataString(exception.Message)}");
            return Task.CompletedTask;
        }

        context.Response.ContentType = "application/json";

        var response = new { message = exception.Message, type = exception.GetType().Name };

        return exception switch
        {
            ValidationException validationException =>
                HandleValidationException(context, validationException),
            DomainException domainException =>
                HandleDomainException(context, domainException, response),
            NotFoundException =>
                HandleNotFoundException(context, response),
            _ => HandleGenericException(context, response)
        };
    }

    private static Task HandleValidationException(HttpContext context, ValidationException exception)
    {
        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        var errors = exception.Errors.Select(f => new { field = f.PropertyName, message = f.ErrorMessage });
        var response = new { message = "Validation failed", errors };
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleDomainException(HttpContext context, DomainException exception, object response)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleNotFoundException(HttpContext context, object response)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return context.Response.WriteAsJsonAsync(response);
    }

    private static Task HandleGenericException(HttpContext context, object response)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsJsonAsync(response);
    }
}

