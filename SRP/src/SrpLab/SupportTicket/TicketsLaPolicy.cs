namespace SrpLab.SupportTicket;

public sealed class TicketSlaPolicy
{
    public DateTimeOffset CalculateDeadline(
    DateTimeOffset openedAt,
    string priority)
    {
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };

        return openedAt.AddHours(hours);
    }

    public bool IsBreached(
        DateTimeOffset openedAt,
        string priority,
        DateTimeOffset now)
    {
        return now > CalculateDeadline(openedAt, priority);
    }

}