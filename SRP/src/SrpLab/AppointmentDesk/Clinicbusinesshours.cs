namespace SrpLab.AppointmentDesk;

public sealed class ClinicBusinessHours
{
    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public ClinicBusinessHours(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        // Clinic calendar policy 
        if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday) return false;
        var t = TimeOnly.FromDateTime(when.DateTime);
        return t >= Open && t.AddMinutes(SlotMinutes) <= Close;
    }
}

//only responsible for clinic calendar policy