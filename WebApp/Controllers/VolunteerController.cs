using E.Application.CQRS.Volunteer.Command.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class VolunteerController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateVolunteerCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVolunteerCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var request = new DeleteVolunteerCommandRequest(id);
        return Ok(await Sender.Send(request));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok();
    }

}
