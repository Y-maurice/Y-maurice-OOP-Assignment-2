namespace SrpLab.AppointmentDesk;

public sealed class AppointmentSmsReminderGenerator
{
    public string Generate(DateTimeOffset slot, string clinicPhone)
    {

        return $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
    }
}