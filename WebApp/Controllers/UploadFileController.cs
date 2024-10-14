using E.Application.CQRS.UploadFile.Command.Request;
using E.Application.CQRS.UploadFile.Query.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class UploadFileController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] CreateUploadFileCommandRequest request)
    {
        return Ok(await Sender.Send(request));
    }

    [HttpGet]
    [Route("download/{id}")]
    public async Task<IActionResult> DownloadFile([FromRoute] int id)
    {
        var request = new OpenFileLinkQueryRequest(id);
        var response = await Sender.Send(request);

        HttpContext.Response.Headers.Add("X-Frame-Options", "ALLOW-FROM *");

        return File(System.IO.File.OpenRead(response.Data.FilePath),
            contentType: response.Data.MimeType

            );
    }


}
