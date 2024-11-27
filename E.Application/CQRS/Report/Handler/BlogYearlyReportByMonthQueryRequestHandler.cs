using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Report.Query.Request;
using E.Application.CQRS.Report.Query.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace E.Application.CQRS.Report.Handler;

public class BlogYearlyReportByMonthQueryRequestHandler : IRequestHandler<BlogYearlyReportByMonthQueryRequest, ResponseModelPagination<BlogYearlyReportByMonthQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public BlogYearlyReportByMonthQueryRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseModelPagination<BlogYearlyReportByMonthQueryResponse>> Handle(BlogYearlyReportByMonthQueryRequest request, CancellationToken cancellationToken)
    {
        var currentYear = DateTime.Now.Year;
        var blogs = _unitOfWork.BlogRepository.GetAll();
        blogs = blogs.Where(x => (request.Year != null) ? x.CreatedDate.Year == request.Year : x.CreatedDate.Year == currentYear);

        var result = new List<BlogYearlyReportByMonthQueryResponse>();

        for (int i = 1; i <= 12; i++)
        {
            var months = new DateTime(currentYear, i, 1);

            var blogsByMonth = blogs.Where(d => d.CreatedDate.Date.Month == i).ToList();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            var response = new BlogYearlyReportByMonthQueryResponse
            {
                Year = request.Year != null ? (int)request.Year : currentYear,
                Month = textInfo.ToTitleCase(months.ToString("MMMM", CultureInfo.CreateSpecificCulture("az-latn-AZ"))),
                Count = blogsByMonth.Count,
            };

            result.Add(response);
        }

        var pagination = new Pagination<BlogYearlyReportByMonthQueryResponse>()
        {
            Datas = result,
            TotalDataCount = await blogs.CountAsync()
        };

        return new ResponseModelPagination<BlogYearlyReportByMonthQueryResponse>
        {
            Data = pagination
        };
    }
}
