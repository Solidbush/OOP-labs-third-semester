namespace Isu.Extra.Exceptions;

public class StudentAlreadyEnrollOnOgnpException : Exception
{
    public StudentAlreadyEnrollOnOgnpException()
        : base("Student already enroll on this Ognp")
    {
    }

    public StudentAlreadyEnrollOnOgnpException(string message)
        : base(message)
    {
    }
}