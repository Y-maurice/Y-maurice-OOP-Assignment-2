namespace SrpLab.KitchenTicket;

public sealed class KitchenEtaEstimator
{
    public int Estimate(
        IReadOnlyList<KitchenItem> items,
        int openStations,
        bool hasAllergens)
    {
        if (openStations <= 0)
            openStations = 1;

        int sequential = items.Sum(i => i.PrepMinutes);

        int parallel = (int)Math.Ceiling(
            sequential / (double)openStations);

        if (hasAllergens)
            parallel += 3;

        int longest = items.Count == 0
            ? 0
            : items.Max(i => i.PrepMinutes);

        return Math.Max(parallel, longest);
    }
}