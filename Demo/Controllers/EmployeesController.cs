using Demo.Contracts;
using Demo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeesController(IEmployeeRepo employeeRepo) => 
            _employeeRepo = employeeRepo;

        [HttpPost]
        public ActionResult<string> Post([FromBody]CreateEmployeeVM employeeVM)
        {
            var result = _employeeRepo.AddEmployee(employeeVM);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }

        [HttpGet("{id:long}")]
        public ActionResult<EmployeeDetailVM> Get(long id)
        {
            var result = _employeeRepo.GetEmployeeById(id);

            if (result == null)
                return BadRequest($"Employeee with ID '{id}' not found.");
            else
                return Ok(result);
        }
    }
}