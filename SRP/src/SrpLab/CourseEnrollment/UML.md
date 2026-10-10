# Course Enrollment — UML


```mermaid
classDiagram
    class CourseEnrollmentDesk {
        -HashSet~string~ _seated
        -Queue~string~ _waitlist
        -WelcomePacketMarkdownGenerator _welcomePacketGenerator
        -TuitionInvoiceLineGenerator _tuitionInvoiceGenerator
        +string CourseCode
        +int Capacity
        +decimal Tuition
        +Register(string studentEmail) string
        +WaitlistPosition(string studentEmail) int
        +PromoteFromWaitlist() string
        +WelcomePacketMarkdown(string studentEmail, string studentName) string
        +TuitionInvoiceLine(string studentEmail) string
    }

    class WelcomePacketMarkdownGenerator {
        +Generate(string courseCode, string studentName, string status) string
    }

    class TuitionInvoiceLineGenerator {
        +Generate(string courseCode, decimal tuition, bool isSeated) string
    }

    CourseEnrollmentDesk --> WelcomePacketMarkdownGenerator :  welcome generation
    CourseEnrollmentDesk --> TuitionInvoiceLineGenerator :  invoice formatting
```

## Responsibilities

- **CourseEnrollmentDesk:** Maintains enrollment state, registers students, manages the waitlist, and promotes students.
- **WelcomePacketMarkdownGenerator:** Generates the welcome packet in Markdown.
- **TuitionInvoiceLineGenerator:** Formats tuition, VAT, and total amounts into an invoice line.

## SRP Refactoring

The enrollment class retains its core state and registration behavior. Changes to welcome packet formatting or invoice formatting can be made independently in their respective classes.

The original public methods remain available for compatibility with existing callers