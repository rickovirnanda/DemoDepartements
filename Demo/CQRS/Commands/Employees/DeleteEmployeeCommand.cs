using Demo.Contracts;
using Demo.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.CQRS.Commands.Employees
{
    public class DeleteEmployeeCommand:IRequest<SuccessResponse>
    {
        public long Id { get; set; }
    }

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, SuccessResponse>
    {
        IEmployeeRepository _employeeRepository;
        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<SuccessResponse> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse();

            var employee = _employeeRepository.GetById(command.Id);

            if (employee == null)
                result.Reason = $"Employee with ID = '{command.Id}' not found.";
            else
            {
                _employeeRepository.Delete(employee);
                result.Success = true;
            }

            return Task.Run(() => result);
        }
    }
}
