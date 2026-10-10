namespace Library_System;

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
}
