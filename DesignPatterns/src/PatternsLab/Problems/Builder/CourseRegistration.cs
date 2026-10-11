namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    public CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email required");
        if (string.IsNullOrWhiteSpace(courseCode)) throw new ArgumentException("course required");

        if (accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");
        if (accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");

        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudentUgly()
    {
        return new CourseRegistration(
            "sara@mail.com",
            "SEF-101",
            "LiveGroup",
            "G1",
            "EARLY10",
            true,
            true,
            "Needs evening slot",
            new DateOnly(2026, 10, 1));
    }

    public static CourseRegistration CreateVideosOnlyUgly()
    {
        return new CourseRegistration(
            "ali@mail.com",
            "SEF-101",
            "VideosOnly",
            null,
            null,
            false,
            true,
            null,
            null);
    }


    public static CourseRegistration CreateLiveStudent()
        => CourseRegistrationBuilder
            .For("sara@mail.com", "SEF-101")
            .LiveGroup()
            .WithGroupCode("G1")
            .WithDiscount("EARLY10")
            .WithWhatsApp()
            .WithEmailWelcome()
            .WithMentorNote("Needs evening slot")
            .StartingOn(new DateOnly(2026, 10, 1))
            .Build();

    public static CourseRegistration CreateVideosOnly()
        => CourseRegistrationBuilder
            .For("ali@mail.com", "SEF-101")
            .VideosOnly()
            .WithEmailWelcome()
            .Build();
}


public sealed class CourseRegistrationBuilder
{
    // required
    private readonly string _studentEmail;
    private readonly string _courseCode;
    private string? _accessMode;

    // optional
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    private CourseRegistrationBuilder(string studentEmail, string courseCode)
    {
        _studentEmail = studentEmail;
        _courseCode = courseCode;
    }

    public static CourseRegistrationBuilder For(string studentEmail, string courseCode)
        => new(studentEmail, courseCode);

    public CourseRegistrationBuilder LiveGroup() { _accessMode = "LiveGroup"; return this; }
    public CourseRegistrationBuilder VideosOnly() { _accessMode = "VideosOnly"; return this; }

    // optional (GroupCode is required for LiveGroup, forbidden for VideosOnly) 
    public CourseRegistrationBuilder WithGroupCode(string groupCode) { _groupCode = groupCode; return this; }
    public CourseRegistrationBuilder WithDiscount(string discountCode) { _discountCode = discountCode; return this; }
    public CourseRegistrationBuilder WithWhatsApp(bool send = true) { _sendWhatsApp = send; return this; }
    public CourseRegistrationBuilder WithEmailWelcome(bool send = true) { _sendEmailWelcome = send; return this; }
    public CourseRegistrationBuilder WithMentorNote(string note) { _mentorNote = note; return this; }
    public CourseRegistrationBuilder StartingOn(DateOnly date) { _preferredStart = date; return this; }

    public CourseRegistration Build()
    {
        if (string.IsNullOrWhiteSpace(_studentEmail))
            throw new InvalidOperationException("Student email is required.");
        if (string.IsNullOrWhiteSpace(_courseCode))
            throw new InvalidOperationException("Course code is required.");
        if (_accessMode is null)
            throw new InvalidOperationException("Access mode is required: call LiveGroup() or VideosOnly().");

        if (_accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException("A LiveGroup registration requires a group code: call WithGroupCode(...).");
        if (_accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(_groupCode))
            throw new InvalidOperationException("A VideosOnly registration cannot have a group code.");

        return new CourseRegistration(
            _studentEmail, _courseCode, _accessMode, _groupCode, _discountCode,
            _sendWhatsApp, _sendEmailWelcome, _mentorNote, _preferredStart);
    }
}