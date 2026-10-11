namespace Library_System.Staff;

public class Librarian : Staff
{
    public Librarian(string personId, string fullName, string phone,
                        DateTime hireDate, decimal salary)
           : base(personId, fullName, phone, hireDate, salary, 0m)
    {
    }

    public void ProcessReturn(Loan loan, DateTime returnDate)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));

        loan.Return(returnDate);
    }

    public void MarkLost(Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));

        loan.MarkLost();
    }
}
