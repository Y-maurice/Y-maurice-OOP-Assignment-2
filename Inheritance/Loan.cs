using Library_System.Enums;
using Library_System.Items;
using Library_System.Members;

namespace Library_System;

public class Loan
{
    public string LoanId { get; }

    public DateTime BorrowDate { get; }

    public Member Member { get; }

    public LibraryItem Item { get; }

    public LoanStatus Status { get; private set; }

    public DateTime? ReturnDate { get; private set; }

    internal Loan(string loanId, Member member, LibraryItem item, DateTime borrowDate)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID cannot be empty.");
        if (member == null)
            throw new ArgumentNullException(nameof(member));
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        LoanId = loanId;
        Member = member;
        Item = item;
        BorrowDate = borrowDate;
        Status = LoanStatus.Borrowed;
    }

    public DateTime DueDate
    {
        get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
    }

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate == null)
                return 0m;

            int daysLate = (ReturnDate.Value.Date - DueDate.Date).Days;
            if (daysLate <= 0)
                return 0m;

            decimal fee = daysLate * Item.GetDailyLateFee();
            return fee * (1m - Member.LateFeeDiscount / 100m);
        }
    }

    internal void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only a borrowed loan can be returned. Current status: " + Status);
        if (returnDate < BorrowDate)
            throw new ArgumentException("Return date cannot be earlier than the borrow date");

        Status = LoanStatus.Returned;
        ReturnDate = returnDate;
        Item.MarkReturned();
    }

    internal void MarkLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only a borrowed loan can be marked as lost. Current status: " + Status + ".");

        Status = LoanStatus.Lost;
    }
}
