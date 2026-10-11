namespace Library_System.Staff;

public class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(string personId, string fullName, string phone,
                    DateTime hireDate, decimal salary, string section)
         : base(personId, fullName, phone, hireDate, salary, 0m)
    {
        if (string.IsNullOrWhiteSpace(section))
            throw new ArgumentException("Section cannot be empty.");

        Section = section;
    }

    public void Reassign(string newSection)
    {
        if (string.IsNullOrWhiteSpace(newSection))
            throw new ArgumentException("Section cannot be emtpy");

        Section = newSection;
    }
}
