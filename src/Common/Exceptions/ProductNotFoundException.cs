namespace Common.Exceptions
{
    public class ProductNotFoundException : DomainException
    {
        public ProductNotFoundException(int id) : base($"Product with ID {id} not found.") { }
    }
}