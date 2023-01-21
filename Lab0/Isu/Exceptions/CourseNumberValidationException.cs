namespace Isu.Exceptions;

public class CourseNumberValidationException : Exception
{
    public CourseNumberValidationException()
        : base("Invalid course number")
    {
    }

    public CourseNumberValidationException(string message)
        : base(message)
    {
    }
}