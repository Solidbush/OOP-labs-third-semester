namespace Shops.Exceptions;

public class GetCountException : Exception
{
    public GetCountException()
        : base("Product don't exists in this shop")
    {
    }

    public GetCountException(string productName, int shopId)
        : base($"Product: {productName} don't exists in shop with ID: {shopId}")
    {
    }
}