namespace BLL.Strategies.Notification
{
    public interface INotificationStrategy
    {
        void Send(int customerId, string message);
    }
}