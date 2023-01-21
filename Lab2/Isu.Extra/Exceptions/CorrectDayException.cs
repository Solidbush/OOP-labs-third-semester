using Isu.Entities;

namespace Isu.Extra.Exceptions;

public class CorrectDayException : Exception
{
    public CorrectDayException()
        : base("You can choose only days in English with big first letters")
    {
    }

    public CorrectDayException(string message)
        : base(message)
    {
    }
}