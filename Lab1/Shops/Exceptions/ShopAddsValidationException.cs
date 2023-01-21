namespace Shops.Exceptions;

public class ShopAddsValidationException : Exception
{
    public ShopAddsValidationException()
        : base("Shop adds validation error")
    {
    }

    public ShopAddsValidationException(string message)
        : base(message)
    {
    }
}