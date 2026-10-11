namespace SrpLab.KitchenTicket;

/// <summary>
/// Kitchen ticket: allergen scan from ingredients, cook ETA heuristics, and printed ticket layout.
/// </summary>
public sealed class KitchenTicket
{
    private readonly List<KitchenItem> _items = new();
    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenEtaEstimator _etaEstimator = new();
    private readonly ThermalTicketPrinter _printer = new();
    private readonly ExpoLaneSelector _expoLaneSelector = new();


    public void AddItem(
     string item,
     string[] ingredients,
     int prepMinutes)
    {
        _items.Add(new KitchenItem(item, ingredients, prepMinutes));
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
        return _etaEstimator.Estimate(
            _items,
            openStations,
            DetectAllergens().Count > 0);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var allergens = DetectAllergens();
        var eta = EstimatedReadyMinutes(2);

        return _printer.Render(_items, orderNumber, eta, allergens);
    }

    public string ExpoLaneHint()
    {
        return _expoLaneSelector.Select(
       DetectAllergens().Count > 0,
       EstimatedReadyMinutes(2));
    }
}
