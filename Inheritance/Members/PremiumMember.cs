namespace Library_System.Members;

public class PremiumMember : Member
{
    public PremiumMember(string personId, string fullName, string phone, decimal discount)
        : base(personId, fullName, phone, 10, discount)
    {
    }

    public int ReadingPoints
    {
        get { return CountReturnedLoans() * 5; }
    }
}

