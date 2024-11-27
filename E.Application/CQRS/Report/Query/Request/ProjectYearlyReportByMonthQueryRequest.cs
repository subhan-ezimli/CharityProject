using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Report.Query.Response;
using MediatR;

namespace E.Application.CQRS.Report.Query.Request;

public class ProjectYearlyReportByMonthQueryRequest : IRequest<ResponseModelPagination<ProjectYearlyReportByMonthQueryResponse>>
{
    public int? Year { get; set; }
}
