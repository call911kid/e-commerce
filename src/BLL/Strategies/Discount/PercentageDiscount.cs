namespace BLL.Strategies.Discount
{
    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly decimal _percentage;
        public PercentageDiscount(decimal percentage) => _percentage = percentage;
        public decimal Apply(decimal subTotal) => subTotal * (_percentage / 100);
    }
}