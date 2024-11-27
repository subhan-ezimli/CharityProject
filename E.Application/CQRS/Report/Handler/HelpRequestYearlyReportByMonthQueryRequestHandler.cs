using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Report.Query.Request;
using E.Application.CQRS.Report.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace E.Application.CQRS.Report.Handler;

public class HelpRequestYearlyReportByMonthQueryRequestHandler : IRequestHandler<HelpRequestYearlyReportByMonthQueryRequest, ResponseModelPagination<HelpRequestYearlyReportByMonthQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public HelpRequestYearlyReportByMonthQueryRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseModelPagination<HelpRequestYearlyReportByMonthQueryResponse>> Handle(HelpRequestYearlyReportByMonthQueryRequest request, CancellationToken cancellationToken)
    {
        var currentYear = DateTime.Now.Year;
        var helpRequests = _unitOfWork.HelpRequestRepository.GetAllAsQueryable();
        helpRequests = helpRequests.Where(x => (request.Year != null) ? x.CreatedDate.Year == request.Year : x.CreatedDate.Year == currentYear);

        var result = new List<HelpRequestYearlyReportByMonthQueryResponse>();

        for (int i = 1; i <= 12; i++)
        {
            var months = new DateTime(currentYear, i, 1);

            var helpRequestsByMonth = helpRequests.Where(d => d.CreatedDate.Date.Month == i).ToList();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            var response = new HelpRequestYearlyReportByMonthQueryResponse
            {
                Year = request.Year != null ? (int)request.Year : currentYear,
                Month = textInfo.ToTitleCase(months.ToString("MMMM", CultureInfo.CreateSpecificCulture("az-latn-AZ"))),
                Count = helpRequestsByMonth.Count,
            };

            result.Add(response);
        }

        var pagination = new Pagination<HelpRequestYearlyReportByMonthQueryResponse>()
        {
            Datas = result,
            TotalDataCount = await helpRequests.CountAsync()
        };

        return new ResponseModelPagination<HelpRequestYearlyReportByMonthQueryResponse>
        {
            Data = pagination
        };
    }
}
