namespace Isu.Extra.Exceptions;

public class MaxCountOfStudentsInGroupException : Exception
{
    public MaxCountOfStudentsInGroupException()
        : base("Max count os students in group should be more then min count of students in group")
    {
    }

    public MaxCountOfStudentsInGroupException(string massege)
        : base(massege)
    {
    }
}