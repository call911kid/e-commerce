namespace BLL.Strategies.Notification
{
    public class SMSNotification : INotificationStrategy
    {
        public void Send(int customerId, string message) =>
            Console.WriteLine($"[Notification] SMS sent to Customer {customerId}: {message}");
    }
}