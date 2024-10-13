using E.Application.CQRS.HelpRequest.Command.Request;
using E.Application.CQRS.HelpRequest.Query.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class HelpRequestController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateHelpRequestCommandRequest request)
    {
        return Ok(await Sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllByFilterHelpRequestQueryRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }


}

