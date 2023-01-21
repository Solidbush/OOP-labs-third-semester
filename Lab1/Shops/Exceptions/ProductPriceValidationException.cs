namespace Shops.Exceptions;

public class ProductPriceValidationException : Exception
{
    public ProductPriceValidationException()
        : base("Product price validation error")
    {
    }

    public ProductPriceValidationException(string message)
        : base(message)
    {
    }
}