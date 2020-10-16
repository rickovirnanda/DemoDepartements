using Demo.Contracts;
using Demo.Models;
using Demo.ViewModels;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.CQRS.Commands.Employees
{
    public class UpdateEmployeeCommand:IRequest<SuccessResponse>
    {
        public EmployeeVM Payload { get; set; }
    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, SuccessResponse>
    {
        IEmployeeRepository _employeeRepository;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<SuccessResponse> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse();

            var employee = _employeeRepository.GetById(command.Payload.Id);

            if (employee == null)
                result.Reason = $"Employee with ID = '{command.Payload.Id}' not found";
            else
            {
                employee.FirstName = command.Payload.FirstName;
                employee.LastName = command.Payload.LastName;
                employee.JoinDate = command.Payload.JoinDate;
                employee.DepartementId = command.Payload.DepartementId;

                _employeeRepository.Update(employee);

                result.Success = true;
            }
            return Task.Run(() => result);
        }
    }
}
