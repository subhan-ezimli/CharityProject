namespace E.Application.CQRS.Report.Query.Response;

public class ProjectYearlyReportByMonthQueryResponse
{
    public int Year { get; set; }
    public string Month { get; set; }
    public int Count { get; set; }

}
