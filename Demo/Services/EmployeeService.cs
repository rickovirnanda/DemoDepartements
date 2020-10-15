using Demo.ViewModels;
using DemoService.CQRS.Commands.Employees;
using DemoService.CQRS.Queries.Employees;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using ServiceProto.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoService.Services
{
    public class EmployeeService:ProtoEmployee.ProtoEmployeeBase
    {
        private readonly IMediator _mediator;
        public EmployeeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<SuccessReply> AddEmployee(AddEmployeeMessage request, ServerCallContext context)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand {
                Payload = new CreateEmployeeVM
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    JoinDate = request.JoinDate.ToDateTime(),
                    DepartementId = request.DepartementId
                }
            });

            return new SuccessReply { Success = result.Success, Reason = result.Reason };
        }

        public override async Task<EmployeeListResponse> GetEmployees(GetEmployeeMessage request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetEmployeeQuery
            {
                Page = (int)request.Page,
                ItemsPerPage = (int)request.ItemsPerPage
            });

            var employeeListResponse = new EmployeeListResponse { };

            employeeListResponse.Employees.AddRange(result.Select(x => new EmployeeMessage
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                DepartementId = x.DepartementId,
                JoinDate = Timestamp.FromDateTime(x.JoinDate.ToUniversalTime()),
                Id = x.Id
            }));

            return employeeListResponse;
        }

        public override async Task<EmployeeResponse> GetEmployeeById(GetByIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery { Id = request.Id });

            var employeeResponse = new EmployeeResponse
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                JoinDate = Timestamp.FromDateTime(result.JoinDate.ToUniversalTime()),
                Departement = new EmployeeDepartementResponse {
                    Id = result.Department.Id,
                    Name = result.Department.Name,
                    Location = result.Department.Location
                }
            };

            return employeeResponse;
        }
    }
}
