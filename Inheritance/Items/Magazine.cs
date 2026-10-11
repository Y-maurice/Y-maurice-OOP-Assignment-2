namespace Library_System.Items;

public class Magazine : LibraryItem
{
    public Magazine(string catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, baseLateFee, 4, 0.5m)
    {
    }
}