namespace Isu.Extra.Exceptions;

public class AddOgnpFlowException : Exception
{
    public AddOgnpFlowException()
        : base("Add Ognp flow Exception")
    {
    }

    public AddOgnpFlowException(string message)
        : base(message)
    {
    }
}