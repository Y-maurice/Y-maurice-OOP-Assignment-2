namespace SrpLab.WardBoard;

/// <summary>
/// Hospital ward board: tracks beds, computes acuity scores, drafts nurse handoff notes,
/// and decides which pager code to fire. Looks like "one ward concern" — it is not.
/// </summary>

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    private readonly AcuityScoreCalculator _acuityCalculator = new();
    private readonly PagerAlertPolicy _pagerAlertPolicy = new();
    private readonly PagerLog _pagerLog = new();
    private readonly HandoffNoteGenerator _handoffNoteGenerator = new();
    private readonly WardCensusCsvExporter _censusCsvExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = ScoreAcuity(heartRate, spo2);

        var alert = _pagerAlertPolicy.CreateAlert(bed, _vitalsScore[bed]);
        if (alert is not null)
            _pagerLog.Add(alert);
    }

    public int ScoreAcuity(int heartRate, int spo2)
        => _acuityCalculator.Score(heartRate, spo2);

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return _handoffNoteGenerator.GenerateEmpty(bed);

        return _handoffNoteGenerator.Generate(bed, patient, _vitalsScore[bed]);
    }

    public IReadOnlyList<string> DrainPagerLog()
        => _pagerLog.Drain();

    public string ExportCensusCsv()
    {
        var rows = new List<WardCensusRow>();
        foreach (var bed in _bedPatient.Keys)
            rows.Add(new WardCensusRow(bed, _bedPatient[bed], _vitalsScore[bed]));
        return _censusCsvExporter.Generate(rows);
    }
}