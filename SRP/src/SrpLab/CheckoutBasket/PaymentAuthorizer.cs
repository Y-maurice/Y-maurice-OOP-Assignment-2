namespace SrpLab.CheckoutBasket;


public sealed class PaymentAuthorizer
{
    public string Authorize(decimal grandTotal, string cardLast4, int lineCount)
    {

        var payload = $"{grandTotal:0.00}|{cardLast4}|{lineCount}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}