namespace Backups.Exceptions;

public class CopyFileException : Exception
{
    public CopyFileException()
        : base("File add exception!")
    {
    }

    public CopyFileException(string message)
        : base(message)
    {
    }
}