namespace Shops.Models;

public class CountPricePair
{
    public CountPricePair(CountOfProduct countOfProduct, PriceOfProduct priceOfProduct)
    {
        CountOfProduct = countOfProduct ?? throw new ArgumentNullException(nameof(countOfProduct));
        PriceOfProduct = priceOfProduct ?? throw new ArgumentNullException(nameof(priceOfProduct));
    }

    public CountOfProduct CountOfProduct { get; }
    public PriceOfProduct PriceOfProduct { get; internal set; }
}