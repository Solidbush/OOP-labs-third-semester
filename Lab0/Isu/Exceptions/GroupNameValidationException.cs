namespace Isu.Exceptions;

public class GroupNameValidationException : Exception
{
    public GroupNameValidationException()
        : base("Group name validation error")
    {
    }

    public GroupNameValidationException(string message)
        : base(message)
    {
    }
}