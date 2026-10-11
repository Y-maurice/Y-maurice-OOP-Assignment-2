namespace SrpLab.Loans;

public sealed class LoanDecisionLetterGenerator
{
    public string Generate(
        string applicantName,
        decimal requestedAmount,
        bool isEligible,
        decimal riskScore,
        IReadOnlyList<string> requiredDocuments)
    {
        if (isEligible)
        {
            return $"Dear {applicantName},\n" +
                   $"Your request for {requestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                   $"Please upload: {string.Join("; ", requiredDocuments)}.\n";
        }

        return $"Dear {applicantName},\n" +
               $"We are unable to approve {requestedAmount:C} at this time.\n" +
               $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
    }
}

// one responsiblity : the format of loan decision letter changes
//only if this format changes this class will change 