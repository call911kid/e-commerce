namespace Common.Exceptions
{
    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException(string productName, int requested, int available)
            : base($"Insufficient stock for '{productName}'. Requested: {requested}, Available: {available}.") { }
    }
}