using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TaxManager.Models;

namespace TaxManager.Controllers
{
    [Route("api/[controller]")]
    public class MunicipalitiesController : Controller
    {
        readonly IRepositoryFacade<Municipality> repoFacade;

        public MunicipalitiesController(IRepositoryFacade<Municipality> rf)
        {
            repoFacade = rf;
        }

        [HttpGet]
        public IEnumerable<Municipality> Get()
        {
            return repoFacade.GetAll();
        }

        // PUT api/municipalities
        [HttpPut]
        public void Put([FromBody] IEnumerable<Municipality> municipalities)
        {
            repoFacade.ReplaceAll(municipalities);
        }

        [HttpPut( "{name}/taxes/{start}/{duration}")]
        public IActionResult Put(string name, DateTime start,
            TimeSpan duration, [FromBody]decimal tax)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Municipality municipality = repoFacade.GetByName(name);

            if (municipality == null)
                municipality = new Municipality() { Name = name };

            municipality.AddScheduledTax(tax, start, duration);
            repoFacade.Upsert(municipality);
            return Ok();
        }
    }
}