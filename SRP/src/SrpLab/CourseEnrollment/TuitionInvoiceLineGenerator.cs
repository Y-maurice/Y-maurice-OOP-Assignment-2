namespace SrpLab.CourseEnrollment;

public sealed class TuitionInvoiceLineGenerator
{
    public string Generate(
        string courseCode,
        decimal tuition,
        bool isSeated)
    {
        if (!isSeated)
            return $"{courseCode},WAITLIST,0.00";

        var vat = Math.Round(tuition * 0.14m, 2);

        return $"{courseCode},TUITION,{tuition:0.00},VAT,{vat:0.00},TOTAL,{(tuition + vat):0.00}";
    }
}

//Responsiblity : Generate invoice line