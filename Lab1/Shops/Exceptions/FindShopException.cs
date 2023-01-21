namespace Shops.Exceptions;

public class FindShopException : Exception
{
    public FindShopException()
        : base("Shop didn't found")
    {
    }

    public FindShopException(int shopId)
        : base($"Shop with ID: {shopId} didn't found")
    {
    }
}