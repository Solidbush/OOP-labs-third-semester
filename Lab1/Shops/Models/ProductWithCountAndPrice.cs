using Shops.Entities;

namespace Shops.Models;

public class ProductWithCountAndPrice
{
    public ProductWithCountAndPrice(Product product, int countOfProduct, decimal priceOfProduct)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        CountOfProduct = new CountOfProduct(countOfProduct);
        PriceOfProduct = new PriceOfProduct(priceOfProduct);
    }

    public Product Product { get; }
    public CountOfProduct CountOfProduct { get; }
    public PriceOfProduct PriceOfProduct { get; }
}