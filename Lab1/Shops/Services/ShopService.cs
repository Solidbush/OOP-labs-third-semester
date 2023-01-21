using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopService : IShopService
{
    private readonly Dictionary<Shop, Dictionary<Product, CountPricePair>> _shopsNet;

    public ShopService()
    {
        _shopsNet = new Dictionary<Shop, Dictionary<Product, CountPricePair>>();
    }

    public Shop AddShop(int shopId, string shopName, string shopAdds)
    {
        var tempShop = new Shop(shopId, shopName, shopAdds);
        if (_shopsNet.Keys.Any(shop => shop.Id == shopId))
        {
            throw new AddShopException($"Shop with {shopId} already exists");
        }

        _shopsNet.Add(tempShop, new Dictionary<Product, CountPricePair>());
        return tempShop;
    }

    public Shop FindShop(Shop shop)
    {
        try
        {
            return _shopsNet
                       .Keys
                       .FirstOrDefault(tempShop => tempShop.Id == shop.Id)
                   ?? throw new FindShopException(shop.Id);
        }
        catch
        {
            return null;
        }
    }

    public Product FindProduct(Product product, Shop shop)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        try
        {
            return _shopsNet[shop]
                       .Keys
                       .FirstOrDefault(tempProduct => tempProduct.Name == product.Name)
                   ?? throw new FindProductException(product.Name);
        }
        catch
        {
            return null;
        }
    }

    public Product AddProduct(Shop shop, Product product)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        if (FindProduct(product, shop) != null)
            throw new AddProductException(product.Name);
        _shopsNet[shop].Add(product, new CountPricePair(new CountOfProduct(0), new PriceOfProduct(0)));
        return product;
    }

    public void SetProductPrice(Shop shop, Product product, PriceOfProduct newPriceOfProduct)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        if (FindProduct(product, shop) == null)
            throw new SetProductPriceException(product.Name, shop.Id);
        _shopsNet[shop][product].PriceOfProduct = newPriceOfProduct;
    }

    public PriceOfProduct GetPriceOfProduct(Shop shop, Product product)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        if (FindProduct(product, shop) == null)
            throw new GetPriceException(product.Name, shop.Id);
        return _shopsNet[shop][product].PriceOfProduct;
    }

    public CountOfProduct GetCountOfProduct(Shop shop, Product product)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        if (FindProduct(product, shop) == null)
            throw new GetCountException(product.Name, shop.Id);
        return _shopsNet[shop][product].CountOfProduct;
    }

    public decimal CountPurchasePrice(Shop shop, Buyer buyer)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        decimal tempFinalCost = 0;
        foreach (var tempProductPair in buyer.GetBuyerProducts())
        {
            if (FindProduct(tempProductPair.Product, shop) == null)
                throw new CountPourchaseException(tempProductPair.Product.Name, shop.Id);
            tempFinalCost += GetPriceOfProduct(shop, tempProductPair.Product).Price *
                             tempProductPair.CountOfProduct.Count;
        }

        return tempFinalCost;
    }

    public void MakeSupply(Shop shop, ProductList supply)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        foreach (var productSupply in supply.GetProductList().Where(productSupply => FindProduct(productSupply.Product, shop) == null))
        {
            throw new SupplyException(productSupply.Product.Name, shop.Id);
        }

        foreach (var productSupply in supply.GetProductList())
        {
            _shopsNet[shop][FindProduct(productSupply.Product, shop)] = new CountPricePair(
                new CountOfProduct(GetCountOfProduct(shop, FindProduct(productSupply.Product, shop)).Count +
                                   productSupply.CountOfProduct.Count),
                new PriceOfProduct(productSupply.PriceOfProduct.Price));
        }
    }

    public bool HasBuyerEnoughMoney(decimal productPrice, Buyer buyer)
    {
        return productPrice <= buyer.GetBuyerMoney();
    }

    public bool HasShopEnoughProductForSale(Shop shop, Buyer buyer)
    {
        if (FindShop(shop) == null)
            throw new FindShopException(shop.Id);
        foreach (var productCountPair in buyer.GetBuyerProducts())
        {
            if (FindProduct(productCountPair.Product, shop) == null)
                throw new GetCountException(productCountPair.Product.Name, shop.Id);
            if (GetCountOfProduct(shop, productCountPair.Product).Count < productCountPair.CountOfProduct.Count)
                return false;
        }

        return true;
    }

    public void Buy(Shop shop, Buyer buyer)
    {
        if (HasShopEnoughProductForSale(shop, buyer) == false)
            throw new NotEnoughCountException(shop.Id);
        decimal priceOfSale = CountPurchasePrice(shop, buyer);
        if (HasBuyerEnoughMoney(priceOfSale, buyer) == false)
            throw new BuyerHasNotEnoughMoneyForBuyException();
        foreach (var productPair in buyer.GetBuyerProducts().Where(productPair => FindProduct(productPair.Product, shop) != null))
        {
            Product findProduct = FindProduct(productPair.Product, shop);
            _shopsNet[shop][findProduct] = new CountPricePair(
                new CountOfProduct(_shopsNet[shop][findProduct].CountOfProduct.Count -
                                   productPair.CountOfProduct.Count),
                new PriceOfProduct(_shopsNet[shop][findProduct].PriceOfProduct.Price));
        }

        shop.SetShopMoney(priceOfSale);
        buyer.SetBuyerMoney(priceOfSale);
    }

    public Shop FindBestShopForBuy(Buyer buyer)
    {
        decimal minPrice = decimal.MaxValue;
        decimal tempPrice;
        Shop bestShop = null;
        foreach (var shop in _shopsNet.Keys)
        {
            tempPrice = CountPurchasePrice(shop, buyer);
            if (tempPrice > minPrice || !HasShopEnoughProductForSale(shop, buyer))
                continue;
            minPrice = tempPrice;
            bestShop = shop;
        }

        return bestShop;
    }
}
