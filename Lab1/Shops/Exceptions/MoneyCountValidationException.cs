namespace Shops.Exceptions;

public class MoneyCountValidationException : Exception
{
    public MoneyCountValidationException()
        : base("Money count validation error")
    {
    }

    public MoneyCountValidationException(string message)
        : base(message)
    {
    }
}