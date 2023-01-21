using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopService
{
    Shop AddShop(int shopId, string shopName, string shopAdds);

    Shop FindShop(Shop shop);

    Product FindProduct(Product product, Shop shop);

    Product AddProduct(Shop shop, Product product);

    void SetProductPrice(Shop shop, Product product, PriceOfProduct newPriceOfProduct);

    PriceOfProduct GetPriceOfProduct(Shop shop, Product product);

    CountOfProduct GetCountOfProduct(Shop shop, Product product);

    decimal CountPurchasePrice(Shop shop, Buyer buyer);

    void MakeSupply(Shop shop, ProductList supply);

    bool HasBuyerEnoughMoney(decimal productPrice, Buyer buyer);

    bool HasShopEnoughProductForSale(Shop shop, Buyer buyer);

    void Buy(Shop shop, Buyer buyer);

    Shop FindBestShopForBuy(Buyer buyer);
}