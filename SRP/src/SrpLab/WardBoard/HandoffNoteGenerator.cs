namespace SrpLab.WardBoard;


public class HandoffNoteGenerator
{
    public string GenerateEmpty(int bed) => $"Bed {bed}: empty";

    public string Generate(int bed, string patient, int acuity)
    {
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        // Presentation / narrative format will change without clinical rules changing.
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}