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
            return Ok(instrument);
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
                return InternalServerError();

            return Ok();
        }
    }
}
