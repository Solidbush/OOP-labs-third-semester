namespace Isu.Exceptions;

public class MaxStudentPerGroupException : Exception
{
    public MaxStudentPerGroupException()
        : base("Group already full")
    {
    }

    public MaxStudentPerGroupException(string message)
        : base(message)
    {
    }
}