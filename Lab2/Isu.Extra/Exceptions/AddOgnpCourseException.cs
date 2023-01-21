namespace Isu.Extra.Exceptions;

public class AddOgnpCourseException : Exception
{
    public AddOgnpCourseException()
        : base("Ognp course already exists")
    {
    }

    public AddOgnpCourseException(string message)
        : base(message)
    {
    }
}