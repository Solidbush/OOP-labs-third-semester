namespace Backups.Exceptions;

public class NameValidationException : Exception
{
    public NameValidationException()
        : base("Backup object can't has null name!")
    {
    }

    public NameValidationException(string message)
        : base(message)
    {
    }
}