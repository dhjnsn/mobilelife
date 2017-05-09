using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TaxManager.Models;

namespace TaxManager.Controllers
{
    [Route("api/[controller]")]
    public class AppliedTaxesController : Controller
    {
        readonly IRepositoryFacade<Municipality> repoFacade;

        public AppliedTaxesController(IRepositoryFacade<Municipality> rf)
        {
            repoFacade = rf;
        }

        [HttpGet("{name}/{date}")]
        public IActionResult Get(string name, DateTime date)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Municipality municipality = repoFacade.GetByName(name);
            if (municipality == null)
                return NotFound("Unknown municipality: " + name);

            return Ok(municipality.GetTaxOnDate(date));
        }
    }
}