namespace Isu.Exceptions;

public class GroupExistingExeption : Exception
{
    public GroupExistingExeption()
        : base("Student doesn't exists")
    {
    }

    public GroupExistingExeption(string message)
        : base(message)
    {
    }
}