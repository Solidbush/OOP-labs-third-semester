namespace Isu.Extra.Exceptions;

public class NumberOfGroupValidationException : Exception
{
    public NumberOfGroupValidationException()
        : base("Number of group should be more then min number")
    {
    }

    public NumberOfGroupValidationException(string message)
        : base(message)
    {
    }
}