namespace Library_System.Items;

public class DVD : LibraryItem
{
    public DVD(string catalogNumber, string title, decimal baseLateFee)
           : base(catalogNumber, title, baseLateFee, 7, 2m)
    {
    }
}
