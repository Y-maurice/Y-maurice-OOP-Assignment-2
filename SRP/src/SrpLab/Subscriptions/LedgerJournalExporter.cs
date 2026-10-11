namespace SrpLab.Subscriptions;

public sealed class LedgerJournalExporter
{
    public string Generate(
        string customerId,
        string invoiceNumber,
        decimal proratedAmount)
    {
        return $"{customerId},{invoiceNumber},{proratedAmount:0.00},AR-SUB";
    }
}
