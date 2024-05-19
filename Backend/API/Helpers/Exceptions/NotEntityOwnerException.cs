namespace API.Helpers.Exceptions
{
    public class NotEntityOwnerException : Exception
    {
        public NotEntityOwnerException(string message) : base(message) { }
    }
}
