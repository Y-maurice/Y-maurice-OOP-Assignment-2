using Library_System.Enums;
using Library_System.Items;

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
}
