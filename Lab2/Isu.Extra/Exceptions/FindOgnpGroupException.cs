namespace Isu.Extra.Exceptions;

public class FindOgnpGroupException : Exception
{
    public FindOgnpGroupException()
        : base("Group doesn't exists in course with this flow")
    {
    }

    public FindOgnpGroupException(string message)
        : base(message)
    {
    }
}