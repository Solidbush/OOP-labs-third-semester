namespace Isu.Extra.Exceptions;

public class SameSpecializationException : Exception
{
    public SameSpecializationException()
        : base("Group and Ognp have the same specializations")
    {
    }

    public SameSpecializationException(string message)
        : base(message)
    {
    }
}