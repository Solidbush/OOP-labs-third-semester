namespace Isu.Extra.Exceptions;

public class MaxCountOfGroupsException : Exception
{
    public MaxCountOfGroupsException()
        : base("Count of groups should be more then 1")
    {
    }

    public MaxCountOfGroupsException(string message)
        : base(message)
    {
    }
}