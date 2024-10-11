using E.Application.CQRS.User.Command.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


public class UserController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] RegisterUserCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

}
