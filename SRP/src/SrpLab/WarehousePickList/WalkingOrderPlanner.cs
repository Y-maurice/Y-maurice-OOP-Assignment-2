namespace SrpLab.WarehousePickList;

public sealed class WalkingOrderPlanner
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> Plan(IReadOnlyList<PickLine> lines)
    {

        var sorted = new List<PickLine>();
        foreach (var line in lines)
        {
            var index = sorted.Count;
            while (index > 0 && Compare(sorted[index - 1], line) > 0)
                index--;
            sorted.Insert(index, line);
        }

        var result = new List<(string Aisle, int Bin, string Sku, int Qty)>();
        foreach (var line in sorted)
        {
            var qty = Math.Min(line.QtyNeeded, line.QtyOnHand);
            if (qty > 0)
                result.Add((line.Aisle, line.Bin, line.Sku, qty));
        }
        return result;
    }

    private static int Compare(PickLine a, PickLine b)
    {
        var byAisle = string.Compare(a.Aisle, b.Aisle);
        if (byAisle != 0) return byAisle;
        return a.Bin.CompareTo(b.Bin);
    }
}