namespace Isu.Exceptions;

public class AddStudentException : Exception
{
    public AddStudentException()
        : base("Student already added in group")
    {
    }

    public AddStudentException(string message)
        : base(message)
    {
    }
}