namespace BLL.Strategies.Discount
{
    public interface IDiscountStrategy
    {
        decimal Apply(decimal subTotal);
    }
}