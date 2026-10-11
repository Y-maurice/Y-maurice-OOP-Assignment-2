namespace SrpLab.CheckoutBasket;

public sealed class BasketPricingCalculator
{
    private const decimal GiftWrapFee = 4.99m; // packaging fee policy ≠ coupon parsing

    public decimal SubTotal(IReadOnlyList<BasketLine> lines)
    {
        decimal total = 0m;
        foreach (var line in lines)
            total += line.Price * line.Qty;
        return total;
    }

    public decimal GrandTotal(decimal subTotal, decimal discount, bool giftWrap)
    {
        var total = subTotal - discount;
        if (giftWrap) total += GiftWrapFee;
        return Math.Max(0m, total);
    }
}