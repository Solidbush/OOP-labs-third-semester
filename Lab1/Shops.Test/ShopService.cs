using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;
namespace Shops.Test;

public class TestShopService
{
    private readonly ShopService _shopService;

    public TestShopService()
    {
        _shopService = new ShopService();
    }

    [Fact]
    public void MakeSupplyInShop_ShopHasNewProductsCountsAndPrices()
    {
        var shop = _shopService.AddShop(1111111, "Diksii", "NewNewYourk");
        var product = _shopService.AddProduct(shop, new Product("Chapman"));
        var productOne = _shopService.AddProduct(shop, new Product("Winston"));
        var productTwo = _shopService.AddProduct(shop, new Product("Canabisul"));
        var tempPrice220 = new PriceOfProduct(220);
        var tempPrice180 = new PriceOfProduct(180);
        var tempCount5 = new CountOfProduct(5);
        var tempCount2 = new CountOfProduct(2);
        ProductList supply1 = new ProductList();
        supply1.AddProduct(new Product("Chapman"), tempCount5, tempPrice220);
        supply1.AddProduct(new Product("Winston"), tempCount2, tempPrice180);
        _shopService.MakeSupply(shop, supply1);

        Assert.Equal(_shopService.GetPriceOfProduct(shop, product).Price, tempPrice220.Price);
        Assert.Equal(_shopService.GetCountOfProduct(shop, product).Count, tempCount5.Count);
        Assert.Equal(_shopService.GetPriceOfProduct(shop, productOne).Price, tempPrice180.Price);
        Assert.Equal(_shopService.GetCountOfProduct(shop, productOne).Count, tempCount2.Count);
    }

    [Fact]
    public void MakeSupplyWithUnExistingProductInShop_ThrowException()
    {
        var shop = _shopService.AddShop(1111111, "Diksii", "NewNewYourk");
        var product = _shopService.AddProduct(shop, new Product("Chapman"));
        ProductList supply1 = new ProductList();
        supply1.AddProduct(new Product("Chapmane"), new CountOfProduct(5), new PriceOfProduct(200));
        Assert.Throws<SupplyException>(() => _shopService
            .MakeSupply(shop, supply1));
    }

    [Fact]
    public void AddProductInShopChangePriceOfProduct_ProductHasNewPrice()
    {
        var shop = _shopService.AddShop(1111111, "Diksii", "NewNewYourk");
        var product = _shopService.AddProduct(shop, new Product("Chapman"));
        _shopService.SetProductPrice(shop, product, new PriceOfProduct(10));

        _shopService.SetProductPrice(shop, product, new PriceOfProduct(100));

        Assert.Equal(new PriceOfProduct(100).Price, _shopService.GetPriceOfProduct(shop, product).Price);
    }

    [Fact]
    public void AddSomeShopAndBuyer_ReturnTheCheapestShop()
    {
        var shopOne = _shopService.AddShop(1111111, "Diksii", "NewNewYourk");
        var shopTwo = _shopService.AddShop(2222222, "Diksii", "NewNewYourk");
        var productOne = _shopService.AddProduct(shopOne, new Product("Chapman"));
        var productTwo = _shopService.AddProduct(shopOne, new Product("Winston"));
        var productThree = _shopService.AddProduct(shopTwo, new Product("Chapman"));
        var productFour = _shopService.AddProduct(shopTwo, new Product("Winston"));
        ProductList supply1 = new ProductList();
        supply1.AddProduct(new Product("Chapman"), new CountOfProduct(5), new PriceOfProduct(200));
        supply1.AddProduct(new Product("Winston"), new CountOfProduct(5), new PriceOfProduct(200));
        _shopService
            .MakeSupply(shopOne, supply1);
        ProductList supply2 = new ProductList();
        supply2.AddProduct(new Product("Chapman"), new CountOfProduct(4), new PriceOfProduct(100));
        supply2.AddProduct(new Product("Winston"), new CountOfProduct(4), new PriceOfProduct(100));
        _shopService
            .MakeSupply(shopTwo, supply2);

        Buyer buyer = new Buyer(300);
        buyer.AddProductInBuyer(new Product("Chapman"), new CountOfProduct(2));
        buyer.AddProductInBuyer(new Product("Winston"), new CountOfProduct(2));

        Assert.Equal(shopTwo.Id, _shopService.FindBestShopForBuy(buyer).Id);
    }

    [Fact]
    public void MakePurchoeseOfProducts_CountOfProductAndMoneyCountChange()
    {
        var shop = _shopService.AddShop(1111111, "Diksii", "NewNewYourk");
        var productOne = _shopService.AddProduct(shop, new Product("Chapman"));
        var productTwo = _shopService.AddProduct(shop, new Product("Winston"));
        ProductList supply1 = new ProductList();
        supply1.AddProduct(productOne, new CountOfProduct(5), new PriceOfProduct(200));
        supply1.AddProduct(productTwo, new CountOfProduct(5), new PriceOfProduct(200));
        _shopService
            .MakeSupply(shop, supply1);
        Buyer buyer = new Buyer(3000);
        buyer.AddProductInBuyer(productOne, new CountOfProduct(3));
        buyer.AddProductInBuyer(productTwo, new CountOfProduct(2));
        decimal beginBuyerMoney = buyer.GetBuyerMoney();

        _shopService.Buy(shop, buyer);

        Assert.Equal(2, _shopService.GetCountOfProduct(shop, productOne).Count);
        Assert.Equal(3, _shopService.GetCountOfProduct(shop, productTwo).Count);
        Assert.Equal(shop.GetShopMoney(), _shopService.CountPurchasePrice(shop, buyer));
        Assert.Equal(buyer.GetBuyerMoney(), beginBuyerMoney - _shopService.CountPurchasePrice(shop, buyer));
    }

    [Fact]
    public void MakePurchoeseOfProductsWithEnoughCount_ThrowException()
    {
        var shop = _shopService.AddShop(1111111, "Diksii", "NewNewYourk");
        var productOne = _shopService.AddProduct(shop, new Product("Chapman"));
        var productTwo = _shopService.AddProduct(shop, new Product("Winston"));
        ProductList supply1 = new ProductList();
        supply1.AddProduct(productOne, new CountOfProduct(1), new PriceOfProduct(200));
        supply1.AddProduct(productTwo, new CountOfProduct(5), new PriceOfProduct(200));
        _shopService
            .MakeSupply(shop, supply1);
        Buyer buyer = new Buyer(3000);
        buyer.AddProductInBuyer(productOne, new CountOfProduct(3));
        buyer.AddProductInBuyer(productTwo, new CountOfProduct(2));
        decimal beginBuyerMoney = buyer.GetBuyerMoney();

        Assert.Throws<NotEnoughCountException>(() => _shopService.Buy(shop, buyer));
    }
}