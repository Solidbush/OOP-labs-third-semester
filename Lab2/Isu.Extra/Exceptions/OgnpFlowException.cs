namespace Isu.Extra.Exceptions;

public class OgnpFlowException : Exception
{
    public OgnpFlowException()
        : base("Ognp flow doesn't exists")
    {
    }

    public OgnpFlowException(string message)
        : base(message)
    {
    }
}