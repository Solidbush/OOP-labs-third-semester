namespace Shops.Exceptions;

public class SetProductPriceException : Exception
{
    public SetProductPriceException()
        : base("Product don't exists in this shop")
    {
    }

    public SetProductPriceException(string productName, int shopId)
        : base($"Product: {productName} don't exists in shop with ID: {shopId}")
    {
    }
}