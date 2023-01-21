namespace Isu.Extra.Exceptions;

public class CoupleTimeValidationException : Exception
{
    public CoupleTimeValidationException()
        : base("We has couples from 1 to 8 time position")
    {
    }

    public CoupleTimeValidationException(string message)
        : base(message)
    {
    }
}