namespace SrpLab.SupportTicket;

/// <summary>
/// Support ticket: SLA clocks, NLP-ish priority from free text, and public reply templates.
/// </summary>
public sealed class SupportTicket
{
    private readonly TicketSlaPolicy _slaPolicy = new();
    private readonly TicketPriorityClassifier _priorityClassifier = new();
    private readonly PublicTicketReplyGenerator _publicReplyGenerator = new();
    private readonly TicketEscalationMessageGenerator _escalationMessageGenerator = new();
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;
        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityClassifier.Classify(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaPolicy.CalculateDeadline(OpenedAt, Priority);
    }


    public bool IsBreached(DateTimeOffset now)
    {
        return _slaPolicy.IsBreached(OpenedAt, Priority, now);
    }
    public string DraftPublicReply(string agentName)
    {
        return _publicReplyGenerator.Generate(
            Id,
            Priority,
            agentName,
            SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _escalationMessageGenerator.Generate(
            Id,
            Priority,
            SlaDeadline());
    }
}
