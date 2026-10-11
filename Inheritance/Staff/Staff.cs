namespace Library_System.Staff;

public class Staff : Person
{
    private readonly decimal allowance;

    public DateTime HireDate { get; }
    public decimal MonthlySalary { get; private set; }

    protected Staff(string personId, string fullName, string phone,
                          DateTime hireDate, decimal salary, decimal allowance)
              : base(personId, fullName, phone)
    {
        if (salary <= 0)
            throw new ArgumentException("Salary must be greater than zero.");
        if (allowance < 0)
            throw new ArgumentException("Allowance cannot be negative.");

        HireDate = hireDate;
        MonthlySalary = salary;
        this.allowance = allowance;
    }

    public decimal MonthlyPay => MonthlySalary + allowance;

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new ArgumentException("Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * percentage / 100m;
    }

}
