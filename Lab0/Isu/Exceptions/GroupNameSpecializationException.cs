namespace Isu.Exceptions;

public class GroupNameSpecializationException : Exception
{
    public GroupNameSpecializationException()
        : base("Invalid group specialization")
    {
    }

    public GroupNameSpecializationException(string message)
        : base(message)
    {
    }
}