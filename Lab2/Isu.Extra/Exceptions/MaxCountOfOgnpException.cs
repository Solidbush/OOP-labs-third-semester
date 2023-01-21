namespace Isu.Extra.Exceptions;

public class MaxCountOfOgnpException : Exception
{
    public MaxCountOfOgnpException()
        : base("Student already has enough Ognp courses")
    {
    }

    public MaxCountOfOgnpException(string message)
        : base(message)
    {
    }
}