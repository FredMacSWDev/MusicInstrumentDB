using Microsoft.AspNet.Identity;
using MusicInstrumentDB.Models.FamousMusicianModels;
using MusicInstrumentDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicInstrumentDB.WebAPI.Controllers
{
    public class FamousMusicianController : ApiController
    {
        [Authorize]
        public class NoteController : ApiController
        {
            private FamousMusicianService CreateMusicianService()
            {
                //checking the id of the user (found in application use data table) then takes that id string and parses it into a guid
                var userId = Guid.Parse(User.Identity.GetUserId());
                //takes in the parsed user id as a guid and creates a new note service object assigned to var noteService
                var noteService = new FamousMusicianService(userId);
                //then we give it back
                return noteService;
            }

            public IHttpActionResult Get()
            {
                FamousMusicianService noteService = CreateMusicianService();
                //class instance calls get notes helper method from note service
                var notes = noteService.GetMusicians();
                return Ok(notes);
            }

            public IHttpActionResult Get(int id)
            {
                FamousMusicianService noteService = CreateMusicianService();
                var note = noteService.GetMusicianById(id);
                return Ok(note);
            }

            public IHttpActionResult Post(FamousMusicianCreate note)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = CreateMusicianService();

                if (!service.CreateMusician(note))
                    return InternalServerError();

                return Ok();
            }

            public IHttpActionResult Put(FamousMusicianEdit note)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var service = CreateMusicianService();

                if (!service.UpdateMusician(note))
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
}
