namespace BLL.Strategies.Shipping
{
    public class StandardShipping : IShippingStrategy
    {
        public decimal Calculate(decimal subTotal) => subTotal > 1000 ? 0 : 50;
    }
}