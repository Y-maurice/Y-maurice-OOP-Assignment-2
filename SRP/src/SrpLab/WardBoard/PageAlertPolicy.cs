namespace SrpLab.WardBoard;


public sealed class PagerAlertPolicy
{
    private const int YellowThreshold = 8;

    public string? CreateAlert(int bed, int acuity)
    {
        if (acuity >= YellowThreshold)
            return $"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}";
        return null;
    }
}