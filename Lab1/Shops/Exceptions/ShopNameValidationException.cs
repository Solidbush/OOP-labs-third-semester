namespace Shops.Exceptions;

public class ShopNameValidationException : Exception
{
    public ShopNameValidationException()
        : base("Shop name validation error")
    {
    }

    public ShopNameValidationException(string message)
        : base(message)
    {
    }
}