using Demo.Contracts;
using Demo.CQRS.Queries;
using Demo.ViewModels;
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
        public Task<EmployeeDetailVM> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.GetById(request.Id);

            if(employee == null)
                throw new NotImplementedException();
            else
            {
                var departement = _departementRepository.GetById(employee.DepartementId);

                if(departement == null)
                    throw new NotImplementedException();

                return Task.Run(() => new EmployeeDetailVM
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    JoinDate = employee.JoinDate,
                    DepartementId = employee.DepartementId,
                    Department = new DepartementVM
                    {
                        Id = departement.Id,
                        Location = departement.Location,
                        Name = departement.Name
                    }
                });
            }
        }
    }
}
