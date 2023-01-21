namespace Backups.Exceptions;

public class ExtractFileException : Exception
{
    public ExtractFileException()
        : base("Can't extract files from zip!")
    {
    }

    public ExtractFileException(string message)
        : base(message)
    {
    }
}