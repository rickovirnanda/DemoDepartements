using MediatR;
using Grpc.Core;
using ServiceProto.Departement;
using System.Threading.Tasks;
using Demo.CQRS.Commands.Departements;
using Demo.ViewModels;
using Demo.CQRS.Queries;
using System.Linq;
using Google.Protobuf.WellKnownTypes;

namespace Demo.Services
{
    public class DepartementService : ProtoDepartement.ProtoDepartementBase
    {
        private readonly IMediator _mediator;

        public DepartementService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<SuccessReply> AddDepartement(AddDepartementMessage request, ServerCallContext context)
        {
            var result = await _mediator.Send(new CreateDepartementCommand
            {
                Payload = new CreateDepartemenVM
                {
                    Name = request.Name,
                    Location = request.Location
                }
            });

            return new SuccessReply { Success = result.Success, Reason = result.Reason };
        }

        public override async Task<DepartementResponse> GetDepartementById(GetByIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetDepartementByIdQuery
            {
                Id = request.Id
            });

            var departementResponse = new DepartementResponse
            {
                Id = result.Id,
                Name = result.Name,
                Location = result.Location,
                
            };

            departementResponse.Employees.AddRange(
                result.Employees.Select(
                    x => new DepartementEmployeeResponse
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        JoinDate = Timestamp.FromDateTime(x.JoinDate.ToUniversalTime())
                    }));

            return departementResponse;
        }
    }
}
