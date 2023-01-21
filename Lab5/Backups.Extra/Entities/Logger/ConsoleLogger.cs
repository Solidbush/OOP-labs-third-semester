namespace Backups.Extra.Entities.Logger;

public class ConsoleLogger : ILoggerMessenger
{
    public void SendLog(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));
        Console.WriteLine(message);
    }
}