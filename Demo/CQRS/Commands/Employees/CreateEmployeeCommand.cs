using Demo.Contracts;
using Demo.Models;
using Demo.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.CQRS.Commands.Employees
{
    public class CreateEmployeeCommand:IRequest<SuccessResponse>
    {
        public CreateEmployeeVM Payload { get; set; }
    }

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, SuccessResponse>
    {
        private readonly IEmployeeRepository _repository;
        public CreateEmployeeHandler(IEmployeeRepository repository) 
        {
            _repository = repository;
        }
        public Task<SuccessResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = new SuccessResponse();

            var employees = _repository.GetEntities(x => x.FirstName.ToLower().Contains(request.Payload.FirstName.ToLower()) 
                                                    && x.LastName.ToLower().Contains(request.Payload.LastName.ToLower()));
            if (employees.Count() != 0)
                result.Reason = $"Employee {request.Payload.FirstName} {request.Payload.LastName} already exist !";
            else
            {
                var employee = new Employee
                {
                    FirstName = request.Payload.FirstName,
                    LastName = request.Payload.LastName,
                    DepartementId = request.Payload.DepartementId,
                    JoinDate = request.Payload.JoinDate
                };

                _repository.Create(employee);

                result.Success = true;
            }

            return Task.Run(() => result);
        }
    }
}
