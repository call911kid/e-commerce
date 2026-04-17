namespace BLL.Strategies.Notification
{
    public class EmailNotification : INotificationStrategy
    {
        public void Send(int customerId, string message) =>
            Console.WriteLine($"[Notification] Email sent to Customer {customerId}: {message}");
    }
}