using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LiteDB;

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

    }
}