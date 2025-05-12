using Microsoft.AspNetCore.Http;

namespace FCG.Domain.Exceptions
{
    public class BusinessErrorDetailsException : BaseCustomException
    {
        public BusinessErrorDetailsException(string message)
            : base(StatusCodes.Status400BadRequest, message) { }
    }
}