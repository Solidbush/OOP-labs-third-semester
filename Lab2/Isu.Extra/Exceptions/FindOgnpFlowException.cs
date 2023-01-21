namespace Isu.Extra.Exceptions;

public class FindOgnpFlowException : Exception
{
    public FindOgnpFlowException()
        : base("This flow doesn't exists in this course")
    {
    }

    public FindOgnpFlowException(string message)
        : base(message)
    {
    }
}