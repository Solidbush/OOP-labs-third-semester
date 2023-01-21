namespace Isu.Exceptions;

public class GetStudentException : Exception
{
    public GetStudentException()
        : base("Student doesn't exist")
    {
    }

    public GetStudentException(string message)
        : base(message)
    {
    }
}