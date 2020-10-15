using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGateway.Contracts;
using DemoGateway.Data;
using DemoGateway.ViewModels;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProto.Departement;

namespace DemoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly IDepartementService _departementService;
        public DepartementController(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] CreateDepartementVM request)
        {
            var result = _departementService.AddDepartement(request);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }

        [HttpGet("{Id:long}")]
        public ActionResult<DepartementDetailVM> GetById(long Id)
        {
            try
            {
                var result = _departementService.GetDepartementById(Id);

                return result;
            }
            catch(RpcException ex)
            {
                //rpc return fail
                return NotFound(ex.Status.Detail);
            }
        }

        [HttpGet]
        public ActionResult<List<DepartementVM>> GetList([FromQuery] int page, [FromQuery] int itemsPerPage)
        {
            var result = _departementService.GetDepartements(page, itemsPerPage);

            return result;
        }

        [HttpDelete("{Id:long}")]
        public ActionResult<string> Delete(long Id)
        {
            var result = _departementService.DeleteDepartement(Id);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }

        [HttpPut]
        public ActionResult<string> Update([FromBody]DepartementVM departement)
        {
            var result = _departementService.UpdateDepartement(departement);

            if (result.Success)
                return Ok("Success");
            else
                return BadRequest(result.Reason);
        }
    }
}
