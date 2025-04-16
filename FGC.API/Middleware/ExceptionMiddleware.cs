using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FGC.API.Middleware
{
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
            catch (Exception ex)
            {
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
