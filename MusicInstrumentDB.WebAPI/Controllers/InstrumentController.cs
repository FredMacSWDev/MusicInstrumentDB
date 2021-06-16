using Microsoft.AspNet.Identity;
using MusicInstrumentDB.Models.InstrumentModels;
using MusicInstrumentDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicInstrumentDB.WebAPI.Controllers
{
    [Authorize]
    public class InstrumentController : ApiController
    {
        private InstrumentService CreateInstrumentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var instrumentService = new InstrumentService(userId);
            return instrumentService;
        }

        public IHttpActionResult Get()
        {
            InstrumentService instrumentService = CreateInstrumentService();
            var instruments = instrumentService.GetInstruments();
            return Ok(instruments);
        }

        public IHttpActionResult Post(InstrumentCreate instrument)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateInstrumentService();

            if (!service.CreateInstrument(instrument))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            InstrumentService instrumentService = CreateInstrumentService();
            var instrument = instrumentService.GetInstrumentById(id);
            if (instrument != null)
                return Ok(instrument);

            return BadRequest("There is no instrument by that Id");
        }

        public IHttpActionResult Put(InstrumentEdit instrument)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateInstrumentService();

            if (!service.UpdateInstrument(instrument))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateInstrumentService();

            if (!service.DeleteInstrument(id))
                return BadRequest("Please remove or edit musicians that refer to this instrument Id");

            return Ok();
        }


        
        [HttpGet]
        public IHttpActionResult GetByName(string instrumentName)
        {
            var service = CreateInstrumentService();
            var instruments = service.GetInstrumentByName(instrumentName);
            if (instruments == null)
                return BadRequest("There are no Instruments by that name.");

            return Ok(instruments);
        }
    }
}
