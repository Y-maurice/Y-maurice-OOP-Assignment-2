namespace SrpLab;

public sealed class AllergenDetector
{
    public IReadOnlyList<string> Detect(IEnumerable<string> ingredients)
    {
        var allergens = new List<string>();

        foreach (var ingredient in ingredients)
        {
            var normalized = ingredient.ToLowerInvariant();

            if (normalized is "milk" or "wheat" or "peanut" or "soy" or "egg")
            {
                if (!allergens.Contains(normalized))
                    allergens.Add(normalized);
            }
        }

        return allergens;
    }

}