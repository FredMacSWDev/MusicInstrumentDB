using Microsoft.AspNet.Identity;
using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.FamousMusicianModels;
using MusicInstrumentDB.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MusicInstrumentDB.WebAPI.Controllers
{

    [Authorize]
    public class MusicianController : ApiController
    {
        //[Queryable]
        //public IQueryable<FamousMusician> Get()
        //{
        //    return FamousMusician.AsQueryable();
        //}

        private FamousMusicianService CreateMusicianService()
        {
            //checking the id of the user (found in application use data table) then takes that id string and parses it into a guid
            var userId = Guid.Parse(User.Identity.GetUserId());
            //takes in the parsed user id as a guid and creates a new service object assigned to var
            var musicianService = new FamousMusicianService(userId);
            //then we give it back
            return musicianService;
        }

        public IHttpActionResult Get()
        {
            FamousMusicianService musicianService = CreateMusicianService();
            //class instance calls get helper method from service
            var musician = musicianService.GetMusicians();
            return Ok(musician);
        }

        [HttpGet]
        public IHttpActionResult SearchAsync(string search)
        {
            FamousMusicianService musicianService = CreateMusicianService();
            var musician = musicianService.GetMusicianSearch(search);
            return Ok(musician);
        }

        public IHttpActionResult Get(int id)
        {
            FamousMusicianService musicianService = CreateMusicianService();
            var musician = musicianService.GetMusicianById(id);
            return Ok(musician);
        }

        public IHttpActionResult Post(FamousMusicianCreate musician)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMusicianService();

            if (!service.CreateMusician(musician))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(FamousMusicianEdit musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateMusicianService();

            if (!service.UpdateMusician(musician))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateMusicianService();

            if (!service.DeleteMusician(id))
                return InternalServerError();

            return Ok();
        }

    }
}

