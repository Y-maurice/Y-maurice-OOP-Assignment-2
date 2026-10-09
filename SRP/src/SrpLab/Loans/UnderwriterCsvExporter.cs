namespace SrpLab.Loans;

public sealed class UnderwriterCsvExporter
{
    public string Generate(
        string applicationId,
        int creditScore,
        int employmentMonths,
        bool hasCollateral,
        decimal riskScore,
        bool isEligible)
    {
        return $"{applicationId},{creditScore},{employmentMonths}," +
               $"{(hasCollateral ? 1 : 0)},{riskScore:0.00}," +
               $"{(isEligible ? "Y" : "N")}";
    }
}

// the only responsiblity is to generate csv format required