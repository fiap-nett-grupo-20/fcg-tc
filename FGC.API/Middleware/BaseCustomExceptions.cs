namespace FGC.API.Middleware
{
    public abstract class BaseCustomException : Exception
    {
        public int StatusCode { get; }

        protected BaseCustomException(int statusCode, string message, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
