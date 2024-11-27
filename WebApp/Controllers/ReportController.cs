using E.Application.CQRS.Report.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ReportController : BaseController
{
    [HttpGet]
    [Route("yearlyreportbymonthofprojects")]
    //[Authorize]
    public async Task<IActionResult> YearlyReportByMonthOfProjects(int? year)
    {
        var request = new ProjectYearlyReportByMonthQueryRequest();

        request.Year = year;
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("yearlyreportbymonthofblogs")]
    // [Authorize]
    public async Task<IActionResult> YearlyReportByMonthOfBlogs(int? year)
    {
        var request = new BlogYearlyReportByMonthQueryRequest();

        request.Year = year;
        var response = await Sender.Send(request);
        return Ok(response);
    }


    [HttpGet]
    [Route("yearlyreportbymonthofvolunteers")]
    //[Authorize]
    public async Task<IActionResult> YearlyReportByMonthOfVolunteers(int? year)
    {
        var request = new VolunteerYearlyReportByMonthQueryRequest();

        request.Year = year;
        var response = await Sender.Send(request);
        return Ok(response);
    }


    [HttpGet]
    [Route("yearlyreportbymonthofhelprequests")]
    // [Authorize]
    public async Task<IActionResult> YearlyReportByMonthOfHelpRequests(int? year)
    {
        var request = new HelpRequestYearlyReportByMonthQueryRequest();

        request.Year = year;
        var response = await Sender.Send(request);
        return Ok(response);
    }
}
