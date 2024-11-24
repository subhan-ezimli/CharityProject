using E.Application.CQRS.Project.Command.Request;
using E.Application.CQRS.Project.Query.Request;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProjectCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var request = new DeleteProjectCommandRequest()
        {
            Id = id
        };

        return Ok(await Sender.Send(request));
    }

    //[HttpGet]
    //[Route("{id}")]
    //public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    //{
    //    return Ok();
    //}

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllByFilterProjectQueryRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }
}
