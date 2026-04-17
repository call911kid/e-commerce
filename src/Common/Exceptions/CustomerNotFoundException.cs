namespace Common.Exceptions
{
    public class CustomerNotFoundException : DomainException
    {
        public CustomerNotFoundException(int id) : base($"Customer with ID {id} not found.") { }
        public CustomerNotFoundException(string email) : base($"Customer with email '{email}' not found.") { }
    }
}