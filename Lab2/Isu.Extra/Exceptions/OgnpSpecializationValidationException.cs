namespace Isu.Extra.Exceptions;

public class OgnpSpecializationValidationException : Exception
{
    public OgnpSpecializationValidationException()
        : base("OGNP Course validation Exception")
    {
    }

    public OgnpSpecializationValidationException(string message)
        : base(message)
    {
    }
}