namespace SrpLab.WardBoard;

class PagerLog
{
    private readonly List<string> _entries = new();

    public void Add(string alert) => _entries.Add(alert);

    public IReadOnlyList<string> Drain()
    {
        var copy = new List<string>(_entries);
        _entries.Clear();
        return copy;
    }
}