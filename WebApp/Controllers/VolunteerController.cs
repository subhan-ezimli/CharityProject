using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class VolunteerController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync()
    {
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> AddAsync([FromRoute] int id)
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
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok();
    }

}
