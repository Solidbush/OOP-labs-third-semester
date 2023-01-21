namespace Shops.Exceptions;

public class AddShopException : Exception
{
    public AddShopException()
        : base("Shop already exists")
    {
    }

    public AddShopException(string message)
        : base(message)
    {
    }
}