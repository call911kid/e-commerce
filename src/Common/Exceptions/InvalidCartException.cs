namespace Common.Exceptions
{
    public class InvalidCartException : DomainException
    {
        public InvalidCartException(string message) : base(message) { }
    }
}