using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Report.Query.Response;
using MediatR;

namespace E.Application.CQRS.Report.Query.Request;

public class VolunteerYearlyReportByMonthQueryRequest : IRequest<ResponseModelList<VolunteerYearlyReportByMonthQueryResponse>>
{
    public int? Year { get; set; }
}
