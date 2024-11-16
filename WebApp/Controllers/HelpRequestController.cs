using E.Application.CQRS.Blog.Command.Request;
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

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var request = new DeleteHelpRequestCommandRequest(id);
        return Ok(await Sender.Send(request));

    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateHelpRequestCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

}

