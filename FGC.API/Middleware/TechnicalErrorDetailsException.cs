namespace FGC.API.Middleware
{
    public class TechnicalErrorDetailsException : BaseCustomException
    {
        public TechnicalErrorDetailsException(string message, Exception? innerException = null)
            : base(StatusCodes.Status500InternalServerError, message, innerException) { }
    }
}
