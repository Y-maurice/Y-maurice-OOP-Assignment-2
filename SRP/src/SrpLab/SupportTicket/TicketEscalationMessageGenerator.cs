namespace SrpLab;

public sealed class TicketEscalationMessageGenerator
{
    public string Generate(
        string ticketId,
        string priority,
        DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {ticketId} priority={priority} " +
               $"breachAt={slaDeadline:u} keywords-scanned=yes";
    }
}