namespace BLL.Strategies.Payment
{
    public interface IPaymentStrategy
    {
        Task<bool> ProcessAsync(decimal amount);
    }
}