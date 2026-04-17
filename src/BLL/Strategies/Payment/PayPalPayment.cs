namespace BLL.Strategies.Payment
{
    public class PayPalPayment : IPaymentStrategy
    {
        public async Task<bool> ProcessAsync(decimal amount)
        {
            await Task.Delay(50);
            Console.WriteLine($"[Payment] PayPal processed: {amount:C}");
            return true;
        }
    }
}