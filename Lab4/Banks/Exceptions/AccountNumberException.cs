namespace Backups.Exceptions;

public class AccountNumberException : Exception
{
    public AccountNumberException()
        : base("Unreal account number")
    {
    }

    public AccountNumberException(string message)
        : base(message)
    {
    }
}