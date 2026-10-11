namespace SrpLab.WardBoard;


public sealed class WardCensusCsvExporter
{
    public string Generate(IEnumerable<WardCensusRow> rows)
    {
        // SortedDictionary keeps the rows ordered by bed number.
        var sorted = new SortedDictionary<int, WardCensusRow>();
        foreach (var row in rows)
            sorted[row.Bed] = row;

        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var row in sorted.Values)
            lines.Add($"{row.Bed},{row.PatientId},{row.Acuity}");
        return string.Join('\n', lines);
    }
}