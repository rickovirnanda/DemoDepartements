using Demo.Contracts;
using Demo.CQRS.Queries;
using Demo.ViewModels;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.CQRS.Queries.Employees
{
    public class GetEmployeeByIdQuery:IRequest<EmployeeDetailVM>
    {
        public long Id { set; get; }
    }

    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailVM>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartementRepository _departementRepository;
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, IDepartementRepository departementRepository)
        {
            _employeeRepository = employeeRepository;
            _departementRepository = departementRepository;
        }
        public Task<EmployeeDetailVM> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.GetById(query.Id);

            if (employee == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Data not found"));
            else
            {
                return Task.Run(() => new EmployeeDetailVM
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    JoinDate = employee.JoinDate,
                    DepartementId = employee.DepartementId,
                    Department = new DepartementVM
                    {
                        Id = employee.Departement.Id,
                        Location = employee.Departement.Location,
                        Name = employee.Departement.Name
                    }
                });
            }
        }
    }
}
