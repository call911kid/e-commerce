namespace BLL.Strategies.Discount
{
    public class FixedDiscount : IDiscountStrategy
    {
        private readonly decimal _fixedAmount;
        public FixedDiscount(decimal amount) => _fixedAmount = amount;
        public decimal Apply(decimal subTotal) => Math.Min(_fixedAmount, subTotal);
    }
}