namespace Shops.Exceptions;

public class AddProductException : Exception
{
    public AddProductException()
        : base("Product already exists")
    {
    }

    public AddProductException(string productName)
        : base($"Product: {productName} already exists")
    {
    }
}