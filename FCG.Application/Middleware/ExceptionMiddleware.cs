using FCG.Domain.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System.Net;

namespace FCG.Application.Middleware;

public class ExceptionMiddleware
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
        catch (Exception ex)
        {
            // Log estruturado antes de escrever a resposta
            _logger.LogError(ex,
                "Erro ao processar requisição {@Method} {@Path} {@TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.TraceIdentifier);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            BaseCustomException custom => custom.StatusCode,
            ArgumentException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new
        {
            StatusCode = statusCode,
            Message = exception.Message
        };

        context.Response.StatusCode = statusCode;

        var jsonResponse = JsonConvert.SerializeObject(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}
