namespace Isu.Exceptions;

public class StudentExistingException : Exception
{
    public StudentExistingException()
        : base("Student doesn't exists")
    {
    }

    public StudentExistingException(string message)
        : base(message)
    {
    }
}