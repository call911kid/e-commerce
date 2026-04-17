namespace BLL.Strategies.Payment
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public async Task<bool> ProcessAsync(decimal amount)
        {
            await Task.Delay(50);
            Console.WriteLine($"[Payment] CreditCard processed: {amount:C}");
            return true;
        }
    }
}