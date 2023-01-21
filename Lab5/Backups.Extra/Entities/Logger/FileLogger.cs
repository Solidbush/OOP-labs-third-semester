namespace Backups.Extra.Entities.Logger;

public class FileLogger : ILoggerMessenger
{
    private string _path;
    public FileLogger(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));
        if (!File.Exists(path))
            throw new FileNotFoundException($"File with path: {path} doesn't exists");
        _path = path;
    }

    public void SendLog(string message)
    {
        File.WriteAllText(_path, message);
    }
}