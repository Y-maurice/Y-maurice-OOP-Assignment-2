namespace SrpLab.GradeBook;

/// <summary>
/// Grade book: aggregation, letter-band policy, and transcript export.
/// </summary>


public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);

    private readonly GradeAverageCalculator _averageCalculator = new();
    private readonly LetterGradePolicy _letterPolicy = new();
    private readonly HonorRollPolicy _honorRollPolicy = new();
    private readonly PlainTranscriptGenerator _transcriptGenerator = new();
    private readonly GradeCsvExporter _csvExporter = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list)) return 0m;
        return _averageCalculator.Calculate(list);
    }

    public string Letter(string studentId)
        => _letterPolicy.GetLetter(Average(studentId));

    public bool MeetsHonorRoll(string studentId)
        => _honorRollPolicy.IsEligible(Average(studentId), Letter(studentId));

    public string TranscriptPlain(string studentId, string fullName)
        => _transcriptGenerator.Generate(
            studentId, fullName, Average(studentId), Letter(studentId), MeetsHonorRoll(studentId));

    public string ExportCsv()
    {
        var summaries = new List<GradeSummary>();
        foreach (var id in _scores.Keys)
            summaries.Add(new GradeSummary(id, Average(id), Letter(id), MeetsHonorRoll(id)));
        return _csvExporter.Generate(summaries);
    }
}
