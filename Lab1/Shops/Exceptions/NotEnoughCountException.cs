namespace Shops.Exceptions;

public class NotEnoughCountException : Exception
{
    public NotEnoughCountException()
        : base("Not enough products in shop")
    {
    }

    public NotEnoughCountException(int shopId)
        : base($"In shop with ID: {shopId} not enough products")
    {
    }
}