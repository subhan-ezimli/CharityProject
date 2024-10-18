using E.Application.CQRS.User.Command.Request;
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

}
