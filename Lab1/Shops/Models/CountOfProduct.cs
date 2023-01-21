using Shops.Exceptions;

namespace Shops.Models;

public class CountOfProduct
{
    private const int MinCountOfProduct = 0;
    public CountOfProduct(int count)
    {
        EnsureCountOfProduct(count);
        Count = count;
    }

    public int Count { get; }

    public void EnsureCountOfProduct(int count)
    {
        if (count < MinCountOfProduct)
        {
            throw new ProductCountValidationException("Product count must be non-negative number");
        }
    }
}