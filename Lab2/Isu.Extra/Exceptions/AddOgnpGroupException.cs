namespace Isu.Extra.Exceptions;

public class AddOgnpGroupException : Exception
{
    public AddOgnpGroupException()
        : base("Ognp group already exists")
    {
    }

    public AddOgnpGroupException(string message)
        : base(message)
    {
    }
}