namespace Shops.Exceptions;

public class ShopIdValidationException : Exception
{
    public ShopIdValidationException()
        : base("Shop ID validation error")
    {
    }

    public ShopIdValidationException(string message)
        : base(message)
    {
    }
}