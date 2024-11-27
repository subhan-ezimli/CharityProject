using E.Application.CQRS.User.Command.Request;
using E.Application.CQRS.User.Query.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AuthController : BaseController
{
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("changePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("sessionuser")]
    [Authorize]
    public async Task<IActionResult> SessionUser()
    {
        var request = new SessionUserQueryRequest();
        var response = await Sender.Send(request);
        return Ok(response);
    }


    [HttpPut]
    [Route("updateme")]
    [Authorize]
    public async Task<IActionResult> UpdateMeAsync([FromBody] UpdateMeCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

}
