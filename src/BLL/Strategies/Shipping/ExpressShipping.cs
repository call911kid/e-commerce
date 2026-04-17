namespace BLL.Strategies.Shipping
{
    public class ExpressShipping : IShippingStrategy
    {
        public decimal Calculate(decimal subTotal) => 150;
    }
}