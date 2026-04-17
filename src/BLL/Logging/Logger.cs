namespace BLL.Logging
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new(() => new Logger());
        public static Logger Instance => _instance.Value;
        private static readonly object _lock = new();

        private Logger() { }

        public void LogInfo(string message) => WriteLog("INFO", message);
        public void LogWarning(string message) => WriteLog("WARN", message);
        public void LogError(string message) => WriteLog("ERROR", message);

        public void LogOrderStarted(int customerId) =>
            LogInfo($"Order process started for CustomerId: {customerId}");

        public void LogPaymentProcessed(decimal amount) =>
            LogInfo($"Payment processed successfully. Amount: {amount:C}");

        public void LogOrderCompleted(int orderId) =>
            LogInfo($"Order #{orderId} completed successfully.");

        private void WriteLog(string level, string message)
        {
            var entry = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            lock (_lock)
            {
                Console.WriteLine(entry);
            }
        }
    }
}