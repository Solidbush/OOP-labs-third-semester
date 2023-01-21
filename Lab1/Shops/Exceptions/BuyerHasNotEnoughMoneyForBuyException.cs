namespace Shops.Exceptions;

public class BuyerHasNotEnoughMoneyForBuyException : Exception
{
    public BuyerHasNotEnoughMoneyForBuyException()
        : base("Buyer Hasn't got enough money for make a purches")
    {
    }

    public BuyerHasNotEnoughMoneyForBuyException(string message)
        : base(message)
    {
    }
}