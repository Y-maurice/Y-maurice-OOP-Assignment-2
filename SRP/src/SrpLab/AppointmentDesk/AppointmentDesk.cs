namespace SrpLab.AppointmentDesk;

/// <summary>
/// Appointment desk: opening-hours rules, slot search, and ICS calendar blob generation.
/// </summary>

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();
    private readonly ClinicBusinessHours _businessHours;
    private readonly AppointmentIcsGenerator _icsGenerator = new();
    private readonly AppointmentSmsReminderGenerator _smsReminderGenerator = new();

    public TimeOnly Open => _businessHours.Open;
    public TimeOnly Close => _businessHours.Close;
    public int SlotMinutes => _businessHours.SlotMinutes;

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        _businessHours = new ClinicBusinessHours(open, close, slotMinutes);
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
        => _businessHours.IsWithinBusinessHours(when);

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);
        while (cursor < end)
        {
            if (IsWithinBusinessHours(cursor) && !_booked.Contains(cursor))
                return cursor;
            cursor = cursor.AddMinutes(SlotMinutes);
        }
        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!IsWithinBusinessHours(slot) || _booked.Contains(slot)) return false;
        _booked.Add(slot);
        return true;
    }

    public string ToIcs(DateTimeOffset slot, string patientName, string clinician)
        => _icsGenerator.Generate(slot, SlotMinutes, patientName, clinician);

    public string SmsReminder(DateTimeOffset slot, string clinicPhone)
        => _smsReminderGenerator.Generate(slot, clinicPhone);

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}