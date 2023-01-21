namespace Isu.Extra.Exceptions;

public class ClassroomNumberValidationException : Exception
{
    public ClassroomNumberValidationException()
        : base("Number of classroom should be more then zero number")
    {
    }

    public ClassroomNumberValidationException(string message)
        : base(message)
    {
    }
}