namespace Shops.Exceptions;

public class AddProductInListException : Exception
{
    public AddProductInListException()
        : base("Some arguments is null")
    {
    }

    public AddProductInListException(string message)
        : base(message)
    {
    }
}