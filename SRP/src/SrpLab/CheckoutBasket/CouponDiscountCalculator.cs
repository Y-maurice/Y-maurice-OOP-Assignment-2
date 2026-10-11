namespace SrpLab.CheckoutBasket;


public sealed class CouponDiscountCalculator
{
    public decimal Calculate(string? couponText, decimal subTotal)
    {
        // Parsing marketing strings is a different reason to change than pricing math.
        if (string.IsNullOrWhiteSpace(couponText)) return 0m;
        var t = couponText.Trim().ToUpperInvariant();
        if (t.StartsWith("SAVE") && int.TryParse(t[4..], out var pct) && pct is > 0 and <= 50)
            return Math.Round(subTotal * pct / 100m, 2);
        if (t.Contains("FREESHIP")) return 0m; // ship is elsewhere — still parsed here
        if (t == "WELCOME10") return Math.Min(10m, subTotal);
        return 0m;
    }
}