using Microsoft.AspNet.Identity;
using MusicInstrumentDB.Models.InstrumentFamilyModels;
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
    public class InstrumentFamilyController : ApiController
    {
        public IHttpActionResult Get()
        {
            InstrumentFamilyService instrumentFamilyService = CreateInstrumentFamilyService();
            var instrumentFamilies = instrumentFamilyService.GetInstrumentFamilies();
            return Ok(instrumentFamilies);
        }

        public IHttpActionResult Get(int id)
        {
            InstrumentFamilyService instrumentFamilyService = CreateInstrumentFamilyService();
            var instrumentFamily = instrumentFamilyService.GetInstrumentFamilyById(id);
            return Ok(instrumentFamily);
        }

       
        public IHttpActionResult Post(InstrumentFamilyCreate instrumentFamily)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateInstrumentFamilyService();

            if (!service.CreateInstrumentFamily(instrumentFamily))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(InstrumentFamilyEdit instrumentFamily)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateInstrumentFamilyService();

            if (!service.UpdateInstrumentFamily(instrumentFamily))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateInstrumentFamilyService();

            if (!service.DeleteInstrumentFamily(id))
                return InternalServerError();

            return Ok();
        }

        private InstrumentFamilyService CreateInstrumentFamilyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var instrumentFamilyService = new InstrumentFamilyService(userId);
            return instrumentFamilyService;
        }

    }
}
