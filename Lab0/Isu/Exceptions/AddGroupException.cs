namespace Isu.Exceptions;

public class AddGroupException : Exception
{
    public AddGroupException()
        : base("Group already exists")
    {
    }

    public AddGroupException(string message)
        : base(message)
    {
    }
}