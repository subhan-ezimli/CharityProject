using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Report.Query.Request;
using E.Application.CQRS.Report.Query.Response;
using E.Application.Security;
using MediatR;
using System.Globalization;

namespace E.Application.CQRS.Report.Handler;

public class ProjectYearlyReportByMonthQueryRequestHandler : IRequestHandler<ProjectYearlyReportByMonthQueryRequest, ResponseModelList<ProjectYearlyReportByMonthQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ProjectYearlyReportByMonthQueryRequestHandler(IUserContext userContext, IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseModelList<ProjectYearlyReportByMonthQueryResponse>> Handle(ProjectYearlyReportByMonthQueryRequest request, CancellationToken cancellationToken)
    {

        var currentYear = DateTime.Now.Year;
        var projects = _unitOfWork.ProjectRepository.GetAllAsQueryable();
        projects = projects.Where(x => (request.Year != null) ? x.CreatedDate.Year == request.Year : x.CreatedDate.Year == currentYear);

        var result = new List<ProjectYearlyReportByMonthQueryResponse>();

        for (int i = 1; i <= 12; i++)
        {
            var months = new DateTime(currentYear, i, 1);

            var projectsByMonth = projects.Where(d => d.CreatedDate.Date.Month == i).ToList();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            var response = new ProjectYearlyReportByMonthQueryResponse
            {
                Year = request.Year != null ? (int)request.Year : currentYear,
                Month = textInfo.ToTitleCase(months.ToString("MMMM", CultureInfo.CreateSpecificCulture("az-latn-AZ"))),
                Count = projectsByMonth.Count,
            };

            result.Add(response);
        }

        return new ResponseModelList<ProjectYearlyReportByMonthQueryResponse>
        {
            Data = result
        };
    }
}
