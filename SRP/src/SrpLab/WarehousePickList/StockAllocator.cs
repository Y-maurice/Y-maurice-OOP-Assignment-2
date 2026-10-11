namespace SrpLab.WarehousePickList;


public sealed class StockAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(IReadOnlyList<PickLine> lines)
    {
        var result = new List<(string Sku, int Allocated)>();
        foreach (var line in lines)
        {
            var alloc = Math.Min(line.QtyNeeded, line.QtyOnHand);
            result.Add((line.Sku, alloc));
        }
        return result;
    }

    public IReadOnlyList<string> ShortfallSkus(IReadOnlyList<PickLine> lines)
    {
        var shortfalls = new List<string>();
        foreach (var allocation in Allocate(lines))
        {

            var need = FirstNeedFor(lines, allocation.Sku);
            if (allocation.Allocated < need)
                shortfalls.Add(allocation.Sku);
        }
        return shortfalls;
    }

    private static int FirstNeedFor(IReadOnlyList<PickLine> lines, string sku)
    {
        foreach (var line in lines)
        {
            if (line.Sku == sku)
                return line.QtyNeeded;
        }
        throw new InvalidOperationException($"No line for SKU {sku}");
    }
}