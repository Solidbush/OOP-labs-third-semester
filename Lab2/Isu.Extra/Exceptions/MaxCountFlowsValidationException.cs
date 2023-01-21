namespace Isu.Extra.Exceptions;

public class MaxCountFlowsValidationException : Exception
{
    public MaxCountFlowsValidationException()
        : base("Count of flows should be more then 1")
    {
    }

    public MaxCountFlowsValidationException(string message)
        : base(message)
    {
    }
}