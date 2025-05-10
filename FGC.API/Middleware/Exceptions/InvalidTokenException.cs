namespace FGC.API.Middleware.Exceptions
{
    public class InvalidTokenException : BusinessErrorDetailsException
    {
        public InvalidTokenException(string code, string message) : base(message)
        {
        }
    }
}
