using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Buyer
{
    private const decimal MinCountOfMoney = 0;
    private readonly Dictionary<Product, CountOfProduct> _products;
    private decimal _money;
    public Buyer(decimal money)
    {
        EnsureCountOfMoney(money);
        _products = new Dictionary<Product, CountOfProduct>();
        _money = money;
    }

    public void AddProductInBuyer(Product newProduct, CountOfProduct countOfProduct)
    {
        if ((newProduct == null) || (countOfProduct == null))
            throw new AddProductInBuyerException();
        _products.Add(newProduct, countOfProduct);
    }

    public void EnsureCountOfMoney(decimal money)
    {
        if (money < MinCountOfMoney)
        {
            throw new MoneyCountValidationException("Money count must be non-negative number");
        }
    }

    public void SetBuyerMoney(decimal priceOfPurches)
    {
        if (priceOfPurches > _money)
            throw new MoneyCountValidationException("Money count must be non-negative number");
        _money -= priceOfPurches;
    }

    public IReadOnlyCollection<ProductWithCount> GetBuyerProducts()
    {
        return _products
            .Select(kvp => new ProductWithCount(kvp.Key, kvp.Value.Count))
            .ToArray();
    }

    public decimal GetBuyerMoney()
    {
        return _money;
    }
}