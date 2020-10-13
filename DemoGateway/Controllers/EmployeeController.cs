using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGateway.Data;
using DemoGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] CreateEmployeeVM request)
        {
            var result = _employeeService.AddEmployee(request);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }

        [HttpGet("{Id:long}")]
        public ActionResult<EmployeeDetailVM> GetById(long Id)
        {
            return _employeeService.GetEmployeeById(Id);
        }
    }
}
