namespace Backups.Exceptions;

public class DeleteDirectoryException : Exception
{
    public DeleteDirectoryException()
        : base("Directory doesn't exists")
    {
    }

    public DeleteDirectoryException(string message)
        : base(message)
    {
    }
}