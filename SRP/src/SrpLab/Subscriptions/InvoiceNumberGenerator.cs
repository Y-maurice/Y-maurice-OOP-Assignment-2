namespace SrpLab.Subscriptions;

public sealed class InvoiceNumberGenerator
{
    private static int _invoiceSeq = 1000;

    public string Generate(DateOnly periodStart)
    {
        var n = ++_invoiceSeq;
        return $"INV-{periodStart:yyyyMM}-{n:D5}";
    }
}
