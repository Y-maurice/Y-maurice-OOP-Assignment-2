namespace SrpLab.KitchenTicket;

public sealed class ExpoLaneSelector
{
    public string Select(bool hasAllergens, int etaMinutes)
    {
        if (hasAllergens)
            return "LANE-ALLERGY";

        return etaMinutes > 20
            ? "LANE-SLOW"
            : "LANE-FAST";
    }
}
