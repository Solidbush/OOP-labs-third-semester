namespace Isu.Extra.Exceptions;

public class FlowNumberValidationException : Exception
{
    public FlowNumberValidationException()
        : base("Flow number should be more then min flow number")
    {
    }

    public FlowNumberValidationException(string message)
        : base(message)
    {
    }
}