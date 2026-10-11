namespace SrpLab.KitchenTicket;

public sealed class KitchenItem
{
    public string Name { get; }
    public List<string> Ingredients { get; }
    public int PrepMinutes { get; }

    public KitchenItem(
        string name,
        string[] ingredients,
        int prepMinutes)
    {
        Name = name;
        Ingredients = ingredients
            .Select(i => i.Trim().ToLowerInvariant())
            .ToList();

        PrepMinutes = prepMinutes;
    }
}