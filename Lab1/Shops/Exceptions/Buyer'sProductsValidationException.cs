namespace Shops.Exceptions;

public class BuyersProductsValidationException : Exception
{
    public BuyersProductsValidationException()
        : base("Buyer's Products validation exception")
    {
    }

    public BuyersProductsValidationException(string message)
        : base(message)
    {
    }
}