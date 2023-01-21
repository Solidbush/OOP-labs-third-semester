namespace Banks.Exceptions;

public class UnrealNumberException : Exception
{
    public UnrealNumberException()
        : base("Unreal value of operation number")
    {
    }

    public UnrealNumberException(string message)
        : base(message)
    {
    }
}