namespace FCG.Domain.Exceptions
{
    public abstract class BaseCustomException : Exception
    {
        public int StatusCode { get; }

        protected BaseCustomException(int statusCode, string message, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        protected BaseCustomException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
