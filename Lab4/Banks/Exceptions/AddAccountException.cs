namespace Banks.Exceptions;

public class AddAccountException : Exception
{
    public AddAccountException()
        : base("Account with same number and title already exists")
    {
    }

    public AddAccountException(string message)
        : base(message)
    {
    }
}