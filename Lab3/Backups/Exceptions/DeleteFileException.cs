namespace Backups.Exceptions;

public class DeleteFileException : Exception
{
    public DeleteFileException()
        : base("Can't delete file")
    {
    }

    public DeleteFileException(string message)
        : base(message)
    {
    }
}