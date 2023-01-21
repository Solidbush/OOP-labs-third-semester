namespace Isu.Extra.Exceptions;

public class OgnpCourseExistingException : Exception
{
    public OgnpCourseExistingException()
        : base("Course doesn't exists")
    {
    }

    public OgnpCourseExistingException(string message)
        : base(message)
    {
    }
}