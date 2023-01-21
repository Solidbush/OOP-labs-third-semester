namespace Isu.Extra.Exceptions;

public class IntersectTimeTablesException : Exception
{
    public IntersectTimeTablesException()
        : base("Timetabels has intersect with eatch other")
    {
    }

    public IntersectTimeTablesException(string message)
        : base(message)
    {
    }
}