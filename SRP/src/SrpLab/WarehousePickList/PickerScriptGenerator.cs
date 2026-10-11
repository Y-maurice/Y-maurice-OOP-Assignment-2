namespace SrpLab.WarehousePickList;

public sealed class PickerScriptGenerator
{
    public string Generate(
        IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder,
        IReadOnlyList<string> shortfallSkus)
    {
        var steps = new List<string>();
        for (var i = 0; i < walkingOrder.Count; i++)
        {
            var s = walkingOrder[i];
            steps.Add($"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");
        }

        var warn = shortfallSkus.Count > 0
            ? "SHORTAGES: " + string.Join(", ", shortfallSkus)
            : "SHORTAGES: none";
        return string.Join('\n', steps) + "\n" + warn;
    }
}