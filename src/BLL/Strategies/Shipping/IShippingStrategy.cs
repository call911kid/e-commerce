namespace BLL.Strategies.Shipping
{
    public interface IShippingStrategy
    {
        decimal Calculate(decimal subTotal);
    }
}