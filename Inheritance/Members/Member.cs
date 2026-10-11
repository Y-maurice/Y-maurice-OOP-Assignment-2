using Library_System.Enums;
using Library_System.Items;

namespace Library_System.Members;

public class Member : Person
{
    private readonly List<Loan> _loans = new List<Loan>();

    public int MaxLoans { get; }

    public decimal LateFeeDiscount { get; }

    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

    protected Member(string personId, string fullName, string phone,
                        int maxLoans, decimal discount)
           : base(personId, fullName, phone)
    {
        if (maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be greater than zero");
        if (discount < 0 || discount > 100)
            throw new ArgumentException("Discount must be between 0 and 100");

        MaxLoans = maxLoans;
        LateFeeDiscount = discount;
    }


    public Loan Borrow(LibraryItem item, string loanId, DateTime date)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        if (item.IsWithdrawn)
            throw new InvalidOperationException("A withdrawn item cannot be borrowed");
        if (item.IsOnLoan)
            throw new InvalidOperationException("This item is already on loan");
        if (CountActiveLoans() >= MaxLoans)
            throw new InvalidOperationException(
                "Loan limit reached. This member can borrow at most " + MaxLoans + " items");

        Loan loan = new Loan(loanId, this, item, date);
        _loans.Add(loan);
        item.MarkBorrowed();
        return loan;
    }

    protected int CountReturnedLoans()
    {
        int count = 0;
        foreach (Loan loan in Loans)
        {
            if (loan.Status == LoanStatus.Returned)
                count++;
        }
        return count;
    }

    private int CountActiveLoans()
    {
        int count = 0;
        foreach (Loan loan in Loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                count++;
        }
        return count;
    }
}
