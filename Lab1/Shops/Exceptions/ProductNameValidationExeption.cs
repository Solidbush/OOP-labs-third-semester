namespace Shops.Exceptions;

public class ProductNameValidationExeption : Exception
{
    public ProductNameValidationExeption()
        : base("Product's name validation error")
    {
    }

    public ProductNameValidationExeption(string message)
        : base(message)
    {
    }
}