using Shops.Exceptions;
namespace Shops.Entities;

public class Shop
{
    private const int MinShopIdCount = 999999;
    private const int MaxShopIdCount = 9999999;
    private const int MinShopNameLength = 1;
    private const int MinShopAddsLength = 10;
    private const decimal MinPriceOfProduct = 0;
    private decimal _money;

    public Shop(int id, string name, string adds)
    {
        EnsureShoptId(id);
        EnsureShopName(name);
        EnsureShopAdds(adds);

        Id = id;
        Name = name;
        Adds = adds;
        _money = 0;
    }

    public int Id { get; }
    public string Name { get; }
    public string Adds { get; }

    public void EnsureShoptId(int shopId)
    {
        if ((shopId <= MinShopIdCount) | (shopId > MaxShopIdCount))
        {
            throw new ShopIdValidationException("The shop's ID must be seven numbers long");
        }
    }

    public void EnsureShopName(string shopName)
    {
        if (string.IsNullOrWhiteSpace(shopName))
        {
            throw new ShopNameValidationException("Shop's name must not be an empty string or has white spaces");
        }

        shopName = shopName.Trim();
        if (shopName.Length < MinShopNameLength)
        {
            throw new ShopNameValidationException("The shop's name must be more than five characters long");
        }
    }

    public void EnsureShopAdds(string shopAdds)
    {
        if (string.IsNullOrWhiteSpace(shopAdds))
        {
            throw new ShopAddsValidationException("Shop's adds must not be an empty string or has white spaces");
        }

        shopAdds = shopAdds.Trim();
        if (shopAdds.Length <= MinShopAddsLength)
        {
            throw new ShopAddsValidationException("The shop's adds must be more than ten characters long");
        }
    }

    public void SetShopMoney(decimal priceOfPurches)
    {
        if (priceOfPurches < MinPriceOfProduct)
        {
            throw new MoneyCountValidationException("Money count must be non-negative number");
        }

        _money += priceOfPurches;
    }

    public decimal GetShopMoney()
    {
        return _money;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Shop tempShop = (Shop)obj;
        return Id == tempShop.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Adds, Id, Name);
    }
}