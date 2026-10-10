# Support Ticket — UML



```mermaid
classDiagram
    class SupportTicket {
        +string Id
        +string Subject
        +string Body
        +DateTimeOffset OpenedAt
        +string Priority
        +AppendCustomerMessage(string text) void
        +RecalculatePriorityFromText() void
        +SlaDeadline() DateTimeOffset
        +IsBreached(DateTimeOffset now) bool
        +DraftPublicReply(string agentName) string
        +InternalEscalationBlurb() string
    }

    class TicketPriorityClassifier {
        +Classify(string subject, string body) string
    }

    class TicketSlaPolicy {
        +CalculateDeadline(DateTimeOffset openedAt, string priority) DateTimeOffset
        +IsBreached(DateTimeOffset openedAt, string priority, DateTimeOffset now) bool
    }

    class PublicTicketReplyGenerator {
        +Generate(string ticketId, string priority, string agentName, DateTimeOffset slaDeadline) string
    }

    class TicketEscalationMessageGenerator {
        +Generate(string ticketId, string priority, DateTimeOffset slaDeadline) string
    }

    SupportTicket --> TicketPriorityClassifier : delegates classification
    SupportTicket --> TicketSlaPolicy : delegates SLA calculations
    SupportTicket --> PublicTicketReplyGenerator : delegates public reply formatting
    SupportTicket --> TicketEscalationMessageGenerator : delegates escalation formatting
```

## Responsibilities

- **SupportTicket:** Maintains ticket state, appends customer messages, and coordinates ticket operations.
- **TicketPriorityClassifier:** Classifies ticket priority as P1, P2, or P3 based on keywords.
- **TicketSlaPolicy:** Calculates SLA deadlines and checks whether they have been breached.
- **PublicTicketReplyGenerator:** Generates customer-facing replies.
- **TicketEscalationMessageGenerator:** Generates internal escalation messages.

## SRP Refactoring

Ticket state and coordination remain in `SupportTicket`. Each extracted class owns one supporting responsibility, allowing classification rules, SLA policies, and message formats to change independently.

The original public methods remain available and delegate to the appropriate helper classes.