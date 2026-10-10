namespace SrpLab.KitchenTicket;

/// <summary>
/// Kitchen ticket: allergen scan from ingredients, cook ETA heuristics, and printed ticket layout.
/// </summary>
public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();
    private readonly AllergenDetector _allergenDetector = new();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        // Regulatory allergen dictionary changes separately from ticket layout.
        var ingredients = new List<string>();

        foreach (var item in _items)
        {
            foreach (var ingredient in item.Ingredients)
            {
                ingredients.Add(ingredient);
            }
        }

        return _allergenDetector.Detect(ingredients);
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        // Kitchen ops model ≠ printing.
        if (openStations <= 0) openStations = 1;
        var sequential = _items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (DetectAllergens().Count > 0) parallel += 3; // allergy protocol delay mixed in
        var longest = _items.Count == 0 ? 0 : _items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        // Hardware/formatting concerns — width, separators — change with printer vendor.
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', _items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergens = DetectAllergens();
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {EstimatedReadyMinutes(2)} MIN\n{body}\n{allergyLine}\n{line}\n";
    }

    public string ExpoLaneHint()
    {
        return DetectAllergens().Count > 0 ? "LANE-ALLERGY" : EstimatedReadyMinutes(2) > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}
