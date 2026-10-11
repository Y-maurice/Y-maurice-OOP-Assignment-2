namespace SrpLab.GradeBook;


public sealed class GradeAverageCalculator
{
    public decimal Calculate(IReadOnlyList<decimal> scores)
    {
        if (scores.Count == 0) return 0m;

        decimal sum = 0m;
        foreach (var score in scores)
            sum += score;

        return Math.Round(sum / scores.Count, 2);
    }
}