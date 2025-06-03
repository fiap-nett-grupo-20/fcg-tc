using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using FCG.Application.Wrappers;
using Microsoft.AspNetCore.Http;

namespace FCG.Application.Middleware
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);

            //if (context.Response.StatusCode is >= 200 and < 300 && context.Response.ContentType?.Contains("application/json") == true)
            //{
            //    context.Response.Body.Seek(0, SeekOrigin.Begin);
            //    var originalResponseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            //    context.Response.Body.Seek(0, SeekOrigin.Begin);

            //    var wrappedResponse = ResponseWrapper<object>.SuccessResponse(
            //        JsonSerializer.Deserialize<object>(originalResponseBody)!,
            //        "Operação realizada com sucesso.",
            //        context.Response.StatusCode
            //    );

            //    var jsonResponse = JsonSerializer.Serialize(wrappedResponse);
            //    var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

            //    context.Response.Body = originalBodyStream;
            //    context.Response.ContentLength = responseBytes.Length;
            //    await context.Response.Body.WriteAsync(responseBytes);
            //}
            //else
            //{
            //    context.Response.Body.Seek(0, SeekOrigin.Begin);
            //    await responseBody.CopyToAsync(originalBodyStream);
            //}
        }
    }
}
