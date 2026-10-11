namespace SrpLab.Subscriptions;

/// <summary>
/// Subscription billing: proration math, invoice number minting, and dunning email bodies.
/// </summary>
public sealed class SubscriptionBilling
{
    private readonly InvoiceNumberGenerator _invoiceNumberGenerator = new();
    private readonly DunningEmailGenerator _dunningEmailGenerator = new();
    private readonly LedgerJournalExporter _ledgerJournalExporter = new();

    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        // Finance calendar rules change independently of email copy.
        if (activeFrom <= PeriodStart) return MonthlyPrice;
        if (activeFrom >= PeriodEnd) return 0m;
        var totalDays = PeriodEnd.DayNumber - PeriodStart.DayNumber;
        if (totalDays <= 0) return MonthlyPrice;
        var used = PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(MonthlyPrice * used / totalDays, 2);
    }


    public string NextInvoiceNumber()
    {
        return _invoiceNumberGenerator.Generate(PeriodStart);
    }

    public void RegisterFailedPayment() => FailedPayments++;

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();

        return _dunningEmailGenerator.Generate(
            customerName,
            asOf,
            amount,
            FailedPayments,
            invoice);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        return _ledgerJournalExporter.Generate(
            CustomerId,
            NextInvoiceNumber(),
            Prorate(activeFrom));
    }
}
