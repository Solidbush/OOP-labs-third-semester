using Shops.Exceptions;

namespace Shops.Models;

public class PriceOfProduct
{
    private const decimal MinPriceOfProduct = 0;
    public PriceOfProduct(decimal price)
    {
        EnsurePriceOfProduct(price);
        Price = price;
    }

    public decimal Price { get; }
    public void EnsurePriceOfProduct(decimal price)
    {
        if (price < MinPriceOfProduct)
        {
            throw new ProductPriceValidationException("Product price must be non-negative number");
        }
    }
}