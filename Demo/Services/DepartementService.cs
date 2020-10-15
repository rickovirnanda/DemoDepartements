using MediatR;
using Grpc.Core;
using ServiceProto.Departement;
using System.Threading.Tasks;
using Demo.CQRS.Commands.Departements;
using Demo.ViewModels;
using Demo.CQRS.Queries;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using Demo.CQRS.Commands;
using DemoService.CQRS.Commands.Departements;

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

        public override async Task<DepartementListResponse> GetDepartement(GetDepartementMessage request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetDepartementsQuery {
                Page = (int)request.Page,
                ItemsPerPage = (int)request.ItemsPerPage
            });

            var departementListResponse = new DepartementListResponse { };

            departementListResponse.Departements.AddRange(
                result.Select(x=> new DepartementMessage
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                })
            );

            return departementListResponse;
        }

        public override async Task<SuccessReply> DeleteDepartement(GetByIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteDepartementCommand { Id = request.Id });

            return new SuccessReply { Success = result.Success, Reason = result.Reason };
        }

        public override async Task<SuccessReply> UpdateDepartement(DepartementMessage request, ServerCallContext context)
        {
            var result = await _mediator.Send(new UpdateDepartementCommand
            {
                Payload = new DepartementVM
                {
                    Id = request.Id,
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
