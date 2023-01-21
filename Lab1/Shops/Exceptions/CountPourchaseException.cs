namespace Shops.Exceptions;

public class CountPourchaseException : Exception
{
    public CountPourchaseException()
        : base("Product didn't find in shop")
    {
    }

    public CountPourchaseException(string productName, int shopId)
        : base($"Product: {productName} int shop with ID: {shopId} didn't find")
    {
    }
}