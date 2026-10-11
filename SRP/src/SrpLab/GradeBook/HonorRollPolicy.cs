namespace SrpLab.GradeBook;


public sealed class HonorRollPolicy
{
    public bool IsEligible(decimal average, string letter)
    {
        return average >= 85 && letter is "A" or "B";
    }
}