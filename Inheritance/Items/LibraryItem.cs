namespace Library_System.Items;

public class LibraryItem
{
    private readonly decimal lateFeeMultiplier;

    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }

    public decimal BaseLateFee { get; private set; }

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; private set; }

    protected LibraryItem(string catalogNumber, string title, decimal baseLateFee,
                               int loanPeriodDays, decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number cannot be empty.");
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");
        if (baseLateFee <= 0)
            throw new ArgumentException("Base late fee must be greater than zero.");
        if (loanPeriodDays <= 0)
            throw new ArgumentException("Loan period must be greater than zero.");
        if (lateFeeMultiplier <= 0)
            throw new ArgumentException("Late fee multiplier must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriodDays = loanPeriodDays;
        this.lateFeeMultiplier = lateFeeMultiplier;
    }

    public decimal GetDailyLateFee()
    {
        return BaseLateFee * lateFeeMultiplier;
    }

    internal void SetBaseLateFee(decimal fee)
    {
        if (fee <= 0)
            throw new ArgumentException("Late fee must be greater than zero.");

        BaseLateFee = fee;
    }

    internal void Withdraw()
    {
        IsWithdrawn = true;
    }

    internal void Restore()
    {
        IsWithdrawn = false;
    }

    internal void MarkBorrowed()
    {
        IsOnLoan = true;
    }

    internal void MarkReturned()
    {
        IsOnLoan = false;
    }
}
