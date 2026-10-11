namespace SrpLab.GradeBook;


public readonly record struct GradeSummary(string StudentId, decimal Average, string Letter, bool Honor);