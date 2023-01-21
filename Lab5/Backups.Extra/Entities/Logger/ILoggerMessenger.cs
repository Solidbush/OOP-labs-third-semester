namespace Backups.Extra.Entities.Logger;

public interface ILoggerMessenger
{
    void SendLog(string message);
}