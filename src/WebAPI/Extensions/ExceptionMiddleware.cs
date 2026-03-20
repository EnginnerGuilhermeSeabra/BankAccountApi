using System.Net;
using System.Text.Json;
using TES.Domain.Common;

namespace TES.WebAPI.Extensions;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await HandleExceptionAsync(ctx, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext ctx, Exception ex)
    {
        var (status, message) = ex switch
        {
            NotFoundException => (HttpStatusCode.NotFound, ex.Message),
            DomainException   => (HttpStatusCode.UnprocessableEntity, ex.Message),
            _                 => (HttpStatusCode.InternalServerError, "Ocorreu um erro interno.")
        };

        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = (int)status;

        var payload = JsonSerializer.Serialize(new { error = message });
        return ctx.Response.WriteAsync(payload);
    }
}
