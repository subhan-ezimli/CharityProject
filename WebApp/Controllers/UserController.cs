using E.Application.CQRS.Blog.Query.Request;
using E.Application.CQRS.Project.Command.Request;
using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var request = new DeleteUserCommandRequest()
        {
            Id = id
        };
        return Ok(await Sender.Send(request));
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] UserGetAllByFilterQueryRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }
}
