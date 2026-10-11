# Subscription Billing — UML


```mermaid
classDiagram
    class SubscriptionBilling {
        -InvoiceNumberGenerator _invoiceNumberGenerator
        -DunningEmailGenerator _dunningEmailGenerator
        -LedgerJournalExporter _ledgerJournalExporter
        +string CustomerId
        +decimal MonthlyPrice
        +DateOnly PeriodStart
        +DateOnly PeriodEnd
        +int FailedPayments
        +Prorate(DateOnly activeFrom) decimal
        +NextInvoiceNumber() string
        +RegisterFailedPayment() void
        +DunningEmail(string customerName, DateOnly asOf) string
        +LedgerJournalLine(DateOnly activeFrom) string
    }

    class InvoiceNumberGenerator {
        -int _invoiceSeq$
        +Generate(DateOnly periodStart) string
    }

    class DunningEmailGenerator {
        +Generate(string customerName, DateOnly asOf, decimal amount, int failedPayments, string invoiceNumber) string
    }

    class LedgerJournalExporter {
        +Generate(string customerId, string invoiceNumber, decimal proratedAmount) string
    }

    SubscriptionBilling --> InvoiceNumberGenerator : delegates numbering
    SubscriptionBilling --> DunningEmailGenerator : delegates email formatting
    SubscriptionBilling --> LedgerJournalExporter : delegates ledger formatting
```

## Responsibilities

- **SubscriptionBilling:** Maintains subscription data, calculates prorated charges, and tracks failed payments.
- **InvoiceNumberGenerator:** Generates sequential invoice numbers. Its sequence is static and shared across instances of this generator.
- **DunningEmailGenerator:** Formats payment reminder emails based on failed-payment count.
- **LedgerJournalExporter:** Formats subscription transactions into ledger journal lines.

## SRP Refactoring

Billing calculations and subscription state remain in `SubscriptionBilling`. Number generation and output formatting are delegated to specialized classes.

The original public methods remain available, preserving the existing calling interface