namespace SrpLab.GradeBook;


public sealed class PlainTranscriptGenerator
{
    public string Generate(string studentId, string fullName, decimal average, string letter, bool honor)
    {
        // Registrar document format ≠ grading policy.
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {honor}\n";
    }
}