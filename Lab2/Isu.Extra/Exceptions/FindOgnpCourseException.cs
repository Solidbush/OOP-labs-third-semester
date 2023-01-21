namespace Isu.Extra.Exceptions;

public class FindOgnpCourseException : Exception
{
    public FindOgnpCourseException()
        : base("OGNP course doesn't exists")
    {
    }

    public FindOgnpCourseException(string message)
        : base(message)
    {
    }
}