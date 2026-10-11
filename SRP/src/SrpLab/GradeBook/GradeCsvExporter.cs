namespace SrpLab.GradeBook;


public sealed class GradeCsvExporter
{
    public string Generate(IEnumerable<GradeSummary> summaries)
    {
        var sorted = new SortedDictionary<string, GradeSummary>();
        foreach (var s in summaries)
            sorted[s.StudentId] = s;

        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var s in sorted.Values)
            rows.Add($"{s.StudentId},{s.Average},{s.Letter},{(s.Honor ? 1 : 0)}");
        return string.Join('\n', rows);
    }
}