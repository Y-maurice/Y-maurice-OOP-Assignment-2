namespace SrpLab.Loans;

/// <summary>
/// Loan desk: eligibility arithmetic, required-document lists, and rejection letter prose.
/// </summary>
public sealed class LoanDesk
{
    private readonly LoanDecisionLetterGenerator _decisionLetterGenerator = new();

    private readonly LoanDocumentRequirements _documentRequirements = new();

    private readonly UnderwriterCsvExporter _underwriterCsvExporter = new();
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    public LoanDesk(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore()
    {
        // Risk model will change with risk committee — not with letter templates.
        decimal score = 100m;
        score -= Math.Max(0, 700 - CreditScore) * 0.15m;
        if (EmploymentMonths < 6) score -= 20m;
        if (RequestedAmount > 50_000m && !HasCollateral) score -= 25m;
        if (RequestedAmount > 150_000m) score -= 10m;
        return Math.Clamp(score, 0m, 100m);
    }

    public bool IsEligible() => RiskScore() >= 55m && CreditScore >= 580;

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentRequirements.GetRequiredDocuments(
            RequestedAmount,
            EmploymentMonths,
            HasCollateral,
            IsEligible());
    } // inside is performed by loanDocumentRequirements class


    public string DecisionLetter(string applicantName)
    {
        return _decisionLetterGenerator.Generate(
            applicantName,
            RequestedAmount,
            IsEligible(),
            RiskScore(),
            RequiredDocuments());
    } // now this is made by DecisionLetterGenerator class

    public string UnderwriterCsvRow(string applicationId)
    {
        return _underwriterCsvExporter.Generate(
            applicationId,
            CreditScore,
            EmploymentMonths,
            HasCollateral,
            RiskScore(),
            IsEligible());
    }
    // csv format is made by UnderwriterCsvExporter class
}
