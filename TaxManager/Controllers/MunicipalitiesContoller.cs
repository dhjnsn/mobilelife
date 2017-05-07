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
        readonly LiteRepository repository;

        public MunicipalitiesController(LiteRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IEnumerable<Municipality> Get()
        {
            return repository.Fetch<Municipality>(Query.All());
        }

        // PUT api/municipalities
        [HttpPut]
        public void Put([FromBody] IEnumerable<Municipality> municipalities)
        {
            using (var transaction = repository.Database.BeginTrans())
            {
                repository.Database.DropCollection("municipality");
                repository.Insert(municipalities, "municipality");
                transaction.Commit();
            }
        }
    }
}