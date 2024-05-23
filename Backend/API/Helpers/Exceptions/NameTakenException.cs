namespace API.Helpers.Exceptions
{
    public class NameTakenException : Exception
    {
        public NameTakenException(string message) : base(message) { }
    }
}
