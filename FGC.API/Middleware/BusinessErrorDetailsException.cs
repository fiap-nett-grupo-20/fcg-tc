namespace FGC.API.Middleware
{
    public class BusinessErrorDetailsException : BaseCustomException
    {
        public BusinessErrorDetailsException(string message)
            : base(StatusCodes.Status400BadRequest, message) { }
    }
}