namespace Backups.Extra.Exceptions;

public class PointsCountException : Exception
{
    public PointsCountException()
        : base("Count should be a non-negative number")
    {
    }

    public PointsCountException(string message)
        : base(message)
    {
    }
}