using E.Application.CQRS.Project.Command.Request;
using E.Application.CQRS.Project.Query.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ProjectController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateProjectCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllByFilterProjectQueryRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }
}
