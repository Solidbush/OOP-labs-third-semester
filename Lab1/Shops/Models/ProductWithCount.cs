using Shops.Entities;

namespace Shops.Models;

public class ProductWithCount
{
    public ProductWithCount(Product product, int countOfProduct)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        CountOfProduct = new CountOfProduct(countOfProduct);
    }

    public Product Product { get; }
    public CountOfProduct CountOfProduct { get; }
}