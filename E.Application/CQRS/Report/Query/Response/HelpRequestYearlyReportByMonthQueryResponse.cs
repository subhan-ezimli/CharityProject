namespace E.Application.CQRS.Report.Query.Response
{
    public class HelpRequestYearlyReportByMonthQueryResponse
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int Count { get; set; }
    }
}
