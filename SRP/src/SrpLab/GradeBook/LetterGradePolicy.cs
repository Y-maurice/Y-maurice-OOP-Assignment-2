namespace SrpLab.GradeBook;


public sealed class LetterGradePolicy
{
    public string GetLetter(decimal average)
    {
        // Academic policy bands change with faculty senate — not with CSV layout.
        if (average >= 90) return "A";
        if (average >= 80) return "B";
        if (average >= 70) return "C";
        if (average >= 60) return "D";
        return "F";
    }
}