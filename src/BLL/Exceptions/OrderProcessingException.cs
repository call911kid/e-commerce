namespace BLL.Exceptions
{
    public class OrderProcessingException : DomainException
    {
        public OrderProcessingException(string message) : base(message) { }
        public OrderProcessingException(string message, Exception inner) : base(message, inner) { }
    }
}