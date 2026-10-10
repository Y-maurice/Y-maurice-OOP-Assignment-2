namespace SrpLab;

public sealed class KitchenEtaEstimator
{
    public int Estimate(IEnumerable<int> preparationMinutes)
    {
        var totalMinutes = 0;

        foreach (var minutes in preparationMinutes)
        {
            totalMinutes += minutes;
        }

        return totalMinutes;
    }

}