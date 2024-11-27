using B.Repository.Common;
using C.Common.GlobalResponses.Generics;
using E.Application.CQRS.Report.Query.Request;
using E.Application.CQRS.Report.Query.Response;
using MediatR;
using System.Globalization;

namespace E.Application.CQRS.Report.Handler;

public class VolunteerYearlyReportByMonthQueryRequestHandler : IRequestHandler<VolunteerYearlyReportByMonthQueryRequest, ResponseModelList<VolunteerYearlyReportByMonthQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public VolunteerYearlyReportByMonthQueryRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModelList<VolunteerYearlyReportByMonthQueryResponse>> Handle(VolunteerYearlyReportByMonthQueryRequest request, CancellationToken cancellationToken)
    {
        var currentYear = DateTime.Now.Year;
        var volunteers = _unitOfWork.VolunteerRepository.GetAllAsQueryable();
        volunteers = volunteers.Where(x => (request.Year != null) ? x.CreatedDate.Year == request.Year : x.CreatedDate.Year == currentYear);

        var result = new List<VolunteerYearlyReportByMonthQueryResponse>();

        for (int i = 1; i <= 12; i++)
        {
            var months = new DateTime(currentYear, i, 1);

            var volunteersByMonth = volunteers.Where(d => d.CreatedDate.Date.Month == i).ToList();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            var response = new VolunteerYearlyReportByMonthQueryResponse
            {
                Year = request.Year != null ? (int)request.Year : currentYear,
                Month = textInfo.ToTitleCase(months.ToString("MMMM", CultureInfo.CreateSpecificCulture("az-latn-AZ"))),
                Count = volunteersByMonth.Count,
            };

            result.Add(response);
        }

        return new ResponseModelList<VolunteerYearlyReportByMonthQueryResponse>
        {
            Data = result
        };
    }
}
