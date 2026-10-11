namespace SrpLab.CourseEnrollment;

public sealed class WelcomePacketMarkdownGenerator
{
    public string Generate(
        string courseCode,
        string studentName,
        string status)
    {
        return $"# Welcome to {courseCode}\n" +
               $"Hi {studentName},\n" +
               $"Your status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }
}

// responsiblity : Generating the new welcome packet