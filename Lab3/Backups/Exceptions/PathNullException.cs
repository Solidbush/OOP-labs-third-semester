namespace Backups.Exceptions;

public class PathNullException : Exception
{
    public PathNullException()
        : base("Backup object can't has null path!")
    {
    }

    public PathNullException(string message)
        : base(message)
    {
    }
}