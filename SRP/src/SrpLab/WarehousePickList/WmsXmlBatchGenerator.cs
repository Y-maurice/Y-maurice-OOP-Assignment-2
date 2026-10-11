namespace SrpLab.WarehousePickList;


public sealed class WmsXmlBatchGenerator
{
    public string Generate(string batchId, IReadOnlyList<(string Sku, int Allocated)> allocations)
    {
        var parts = new List<string>();
        foreach (var a in allocations)
            parts.Add($"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}