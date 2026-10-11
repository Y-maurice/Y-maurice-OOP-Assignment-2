using Library_System.Items;

namespace Library_System.Staff;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(string personId, string fullName, string phone,
                           DateTime hireDate, decimal salary)
          : base(personId, fullName, phone, hireDate, salary, 400m)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newFee)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        item.SetBaseLateFee(newFee);
    }

    public void Withdraw(LibraryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        item.Withdraw();
    }

    public void Restore(LibraryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        item.Restore();
    }
}
