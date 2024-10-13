using E.Application.CQRS.UploadFile.Command.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class UploadFileController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] CreateUploadFileCommandRequest request)
    {
        return Ok(await Sender.Send(request));
    }
}
