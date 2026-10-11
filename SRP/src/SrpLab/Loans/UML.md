# Loan Desk — UML


```mermaid
classDiagram
    class LoanDesk {
        +decimal RequestedAmount
        +int CreditScore
        +int EmploymentMonths
        +bool HasCollateral
        +RiskScore() decimal
        +IsEligible() bool
        +RequiredDocuments() IReadOnlyList
        +DecisionLetter(string applicantName) string
        +UnderwriterCsvRow(string applicationId) string
    }

    class LoanDocumentRequirements {
        +GetRequiredDocuments(decimal requestedAmount, int employmentMonths, bool hasCollateral, bool isEligible) IReadOnlyList
    }

    class LoanDecisionLetterGenerator {
        +Generate(string applicantName, decimal requestedAmount, bool isEligible, decimal riskScore, IReadOnlyList requiredDocuments) string
    }

    class UnderwriterCsvExporter {
        +Generate(string applicationId, int creditScore, int employmentMonths, bool hasCollateral, decimal riskScore, bool isEligible) string
    }

    LoanDesk --> LoanDocumentRequirements : delegates document selection
    LoanDesk --> LoanDecisionLetterGenerator : delegates letter generation
    LoanDesk --> UnderwriterCsvExporter : delegates CSV formatting
```

## Responsibilities

- **LoanDesk:** Holds loan application data and determines risk score and eligibility.
- **LoanDocumentRequirements:** Determines which documents an applicant must provide.
- **LoanDecisionLetterGenerator:** Produces approval or rejection letters.
- **UnderwriterCsvExporter:** Formats application details for underwriter CSV output.

## SRP Refactoring

Loan eligibility decisions remain separate from document selection and output formatting. Each extracted class can evolve independently without taking responsibility for the entire loan workflow.

The original `LoanDesk` methods remain available as delegating entry points.