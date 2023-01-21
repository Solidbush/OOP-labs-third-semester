namespace Backups.Exceptions;

public class CreateZipException : Exception
{
    public CreateZipException()
        : base("Can't create zip archive")
    {
    }

    public CreateZipException(string message)
        : base(message)
    {
    }
}