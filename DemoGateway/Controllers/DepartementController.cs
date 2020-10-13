using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGateway.Contracts;
using DemoGateway.Data;
using DemoGateway.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProto.Departement;

namespace DemoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        public readonly ProtoDepartement.ProtoDepartementClient _departement;
        private readonly IDepartementService _departementService;
        public DepartementController(IGrpcClient client, IDepartementService departementService)
        {
            _departement = new ProtoDepartement.ProtoDepartementClient(client.DepartementChannel);
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
        public ActionResult<DepartementResponse> GetById(long Id)
        {
            var result = _departement.GetDepartementById(new GetByIdRequest { Id = Id });

            return result;
        }
    }
}
