namespace Shops.Exceptions;

public class SupplyException : Exception
{
    public SupplyException()
        : base("Product in supply don't exists in shop")
    {
    }

    public SupplyException(string productName, int shopId)
        : base($"Product with name: {productName} don't exists in shop with ID: {shopId}")
    {
    }
}