namespace Backups.Exceptions;

public class CreateDirectoryException : Exception
{
    public CreateDirectoryException()
        : base("Add directory exception")
    {
    }

    public CreateDirectoryException(string message)
        : base(message)
    {
    }
}