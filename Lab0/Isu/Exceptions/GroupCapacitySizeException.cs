namespace Isu.Exceptions;

public class GroupCapacitySizeException : Exception
{
    public GroupCapacitySizeException()
        : base("Invalid size of group capacity")
    {
    }

    public GroupCapacitySizeException(string message)
        : base(message)
    {
    }
}