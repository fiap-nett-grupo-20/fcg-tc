using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Wrappers
{
    public class ResponseWrapper<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ResponseWrapper(bool success, int statusCode, string? message, T? data)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static ResponseWrapper<T> SuccessResponse(T data, string? message = null, int statusCode = 200)
        {
            return new ResponseWrapper<T>(true, statusCode, message, data);
        }

        public static ResponseWrapper<T> FailResponse(string message, int statusCode = 400)
        {
            return new ResponseWrapper<T>(false, statusCode, message, default);
        }

        public static ResponseWrapper<T> ValidationFailResponse(List<string> errors)
        {
            return new ResponseWrapper<T>(false, 400, "Erro de validação", (T)(object)errors);
        }
    }
}
