namespace Backups.Exceptions;

public class DeleteBackupObjectException : Exception
{
    public DeleteBackupObjectException()
        : base("Object can't be delete!")
    {
    }

    public DeleteBackupObjectException(string message)
        : base(message)
    {
    }
}