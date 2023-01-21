namespace Shops.Exceptions;

public class FindProductException : Exception
{
    public FindProductException()
        : base("Product didn't find")
    {
    }

    public FindProductException(string productName)
        : base($"Product with name: {productName} didn't find")
    {
    }
}