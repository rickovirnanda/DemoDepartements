using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGateway.Contracts;
using DemoGateway.ViewModels;
using Grpc.Core;
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

        [HttpPut]
        public ActionResult<string> Update([FromBody]EmployeeVM request)
        {
            var result = _employeeService.UpdateEmployee(request);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }

        [HttpGet("{Id:long}")]
        public ActionResult<EmployeeDetailVM> GetById(long Id)
        {
            try
            {
                return _employeeService.GetEmployeeById(Id);
            }
            catch(RpcException ex)
            {
                return BadRequest(ex.Status.Detail);
            }
        }

        [HttpGet]
        public ActionResult<List<EmployeeVM>> GetList([FromQuery] int page, [FromQuery] int itemsPerPage)
        {
            return _employeeService.GetEmployees(page, itemsPerPage);
        }

        [HttpDelete("{Id:long}")]
        public ActionResult<string> Delete(long Id)
        {
            var result = _employeeService.DeleteEmployee(Id);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }
    }
}
