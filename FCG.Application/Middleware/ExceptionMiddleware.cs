using FCG.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FCG.Application.Middleware
{
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

            
            if (exception is BaseCustomException customException)
            {
                context.Response.StatusCode = customException.StatusCode;
                var jsonResponse = JsonConvert.SerializeObject(new
                {
                    StatusCode = customException.StatusCode,
                    Message = customException.Message
                });
                return context.Response.WriteAsync(jsonResponse);
            }

            if (exception is ArgumentException exceptionArgument)
            {
                //context.Response.StatusCode = exceptionArgument;
                var jsonResponse = JsonConvert.SerializeObject(new
                {
                    //StatusCode = customException.StatusCode,
                    Message = exceptionArgument.Message
                });
                return context.Response.WriteAsync(jsonResponse);
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var defaultResponse = JsonConvert.SerializeObject(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Erro inesperado, contate o administrador do sistema."
            });
            return context.Response.WriteAsync(defaultResponse);
        }
    }
}
