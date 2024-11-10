using E.Application.CQRS.Gallery.Command.Request;
using E.Application.CQRS.Gallery.Query.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace WebApp.Controllers
{
    public class GalleryController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateGalleryCommandRequest request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllGalleriesQueryRequest request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var request = new DeleteGalleryCommandRequest(id);
            return Ok(await Sender.Send(request));
        }


    }
}
