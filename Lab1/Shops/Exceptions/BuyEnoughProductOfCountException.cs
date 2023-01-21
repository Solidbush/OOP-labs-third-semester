namespace Shops.Exceptions;

public class BuyEnoughProductOfCountException : Exception
{
    public BuyEnoughProductOfCountException()
        : base("Shop hasn't got enough products for sell")
    {
    }

    public BuyEnoughProductOfCountException(int shopId)
        : base($"Shop with ID: {shopId} hasn't got enough products for sell")
    {
    }
}