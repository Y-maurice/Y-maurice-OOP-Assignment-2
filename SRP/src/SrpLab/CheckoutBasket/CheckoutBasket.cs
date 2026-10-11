namespace SrpLab.CheckoutBasket;



public sealed class CheckoutBasket
{
    private readonly List<BasketLine> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;

    private readonly BasketPricingCalculator _pricing = new();
    private readonly CouponDiscountCalculator _couponCalculator = new();
    private readonly GiftMessageCardGenerator _giftCardGenerator = new();
    private readonly PaymentAuthorizer _paymentAuthorizer = new();

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add(new BasketLine(sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() => _pricing.SubTotal(_lines);

    public decimal DiscountAmount() => _couponCalculator.Calculate(_couponRaw, SubTotal());

    public decimal GrandTotal() => _pricing.GrandTotal(SubTotal(), DiscountAmount(), _giftWrap);

    public string GiftMessageCard(string fromName)
    {
        var skus = new List<string>();
        foreach (var line in _lines)
            skus.Add(line.Sku);
        return _giftCardGenerator.Generate(fromName, skus, GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
        => _paymentAuthorizer.Authorize(GrandTotal(), cardLast4, _lines.Count);
}
