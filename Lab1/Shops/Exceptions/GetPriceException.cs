using Shops.Entities;

namespace Shops.Exceptions;

public class GetPriceException : Exception
{
    public GetPriceException()
        : base("Product don't exists in this shop")
    {
    }

    public GetPriceException(string productName, int shopId)
        : base($"Product: {productName} don't exists in shop with ID: {shopId}")
    {
    }
}