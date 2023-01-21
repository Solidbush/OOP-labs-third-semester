namespace Shops.Exceptions;

public class AddProductInBuyerException : Exception
{
    public AddProductInBuyerException()
        : base("Some arguments is null")
    {
    }

    public AddProductInBuyerException(string message)
        : base(message)
    {
    }
}