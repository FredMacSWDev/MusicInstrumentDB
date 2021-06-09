using Microsoft.AspNet.Identity;
using MusicInstrumentDB.Models.ImageModels;
using MusicInstrumentDB.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicInstrumentDB.WebAPI.Controllers
{
    [Authorize]
    public class ImageController : ApiController
    {
        private ImageService CreateImageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var imageService = new ImageService(userId);
            return imageService;
        }

        public IHttpActionResult Get(int id)
        {
            ImageService imageService = CreateImageService();
            var image = imageService.GetImageById(id);
            return Ok(image);
        }

        public IHttpActionResult Post(Image image)//looks liks this is asking for every parameter for an image. Needs refactor
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateImageService();
            if (!service.CreateImage(image))
                return InternalServerError();
            return Ok();
        }
    }
}
