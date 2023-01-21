namespace Shops.Exceptions;

public class ProductCountValidationException : Exception
{
    public ProductCountValidationException()
        : base("Product count validation error")
    {
    }

    public ProductCountValidationException(string message)
        : base(message)
    {
    }
}