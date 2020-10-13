using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Contracts;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepo _personRepo;

        public PersonsController(IPersonRepo personRepo) 
        {
            _personRepo = personRepo;
        }

        [HttpGet]
        public ActionResult<List<Person>> Get([FromQuery]int page, [FromQuery]int itemsPerPage)
        {
            var result = _personRepo.GetListPerson(page, itemsPerPage);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<Person> Get(long id)
        {
            var result = _personRepo.GetPersonById(id);

            if (result == null)
                return BadRequest($"Person with ID {id} not found.");
            else
                return Ok(result);
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody]Person person)
        {
            var result = _personRepo.AddPerson(person);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }
    }
}