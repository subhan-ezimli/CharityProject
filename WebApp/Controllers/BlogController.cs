using E.Application.CQRS.Blog.Command.Request;
using E.Application.CQRS.Blog.Query.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class BlogController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateBlogCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateBlogCommandRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var request = new DeleteBlogCommandRequest()
        {
            Id = id
        };

        return Ok(await Sender.Send(request));

    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllByFilterBlogQueryRequest request)
    {
        var response = await Sender.Send(request);
        return Ok(response);
    }

}
