using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class ProductList
{
    private readonly Dictionary<Product, CountPricePair> productList;

    public ProductList()
    {
        productList = new Dictionary<Product, CountPricePair>();
    }

    public void AddProduct(Product newProduct, CountOfProduct countOfProduct, PriceOfProduct priceOfProduct)
    {
        if ((newProduct == null) || (countOfProduct == null) || (priceOfProduct == null))
            throw new AddProductInListException();
        var tempCountPricePair = new CountPricePair(countOfProduct, priceOfProduct);
        productList.Add(newProduct, tempCountPricePair);
    }

    public IReadOnlyCollection<ProductWithCountAndPrice> GetProductList()
    {
        return productList.Select(kvp => new ProductWithCountAndPrice(kvp.Key, kvp.Value.CountOfProduct.Count, kvp.Value.PriceOfProduct.Price))
            .ToArray();
    }
}