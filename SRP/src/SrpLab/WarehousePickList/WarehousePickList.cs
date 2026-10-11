namespace SrpLab.WarehousePickList;

/// <summary>
/// Warehouse pick list: naive path order, stock allocation, and picker instruction prose.
/// </summary>

public sealed class WarehousePickList
{
    private readonly List<PickLine> _lines = new();

    private readonly StockAllocator _allocator = new();
    private readonly WalkingOrderPlanner _walkingOrderPlanner = new();
    private readonly PickerScriptGenerator _pickerScriptGenerator = new();
    private readonly WmsXmlBatchGenerator _wmsXmlBatchGenerator = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add(new PickLine(sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
        => _allocator.Allocate(_lines);

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
        => _walkingOrderPlanner.Plan(_lines);

    public string PickerScript()
        => _pickerScriptGenerator.Generate(WalkingOrder(), _allocator.ShortfallSkus(_lines));

    public string WmsXmlBatch(string batchId)
        => _wmsXmlBatchGenerator.Generate(batchId, Allocate());
}
