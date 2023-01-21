namespace Shops.Exceptions;

public class ShopNetIsNullException : Exception
{
    public ShopNetIsNullException()
        : base("Shop net hasn't got shops for pourchese")
    {
    }

    public ShopNetIsNullException(string message)
        : base(message)
    {
    }
}