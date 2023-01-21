namespace Isu.Extra.Exceptions;

public class ScheduleValidationException : Exception
{
    public ScheduleValidationException()
        : base("Schedule should has couples more then min count of co couples")
    {
    }

    public ScheduleValidationException(string message)
        : base(message)
    {
    }
}