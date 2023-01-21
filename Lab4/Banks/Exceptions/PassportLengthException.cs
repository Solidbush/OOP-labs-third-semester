namespace Banks.Exceptions;

public class PassportLengthException : Exception
{
    public PassportLengthException()
        : base("Passport length should have 10 digits")
    {
    }

    public PassportLengthException(string message)
        : base(message)
    {
    }
}