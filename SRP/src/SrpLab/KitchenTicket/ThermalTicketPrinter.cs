namespace SrpLab.KitchenTicket;

public sealed class ThermalTicketPrinter
{
    public string Render(
        IReadOnlyList<KitchenItem> items,
        int orderNumber,
        int etaMinutes,
        IReadOnlyList<string> allergens)
    {
        var width = 32;
        var line = new string('=', width);

        var body = string.Join(
            '\n',
            items.Select(i =>
                $"* {i.Name.ToUpperInvariant()} ({i.PrepMinutes}m)"));

        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}\nORDER #{orderNumber}\nETA {etaMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}