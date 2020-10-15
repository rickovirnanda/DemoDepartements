using Demo.Contracts;
using Demo.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.CQRS.Queries.Employees
{
    public class GetEmployeeQuery:IRequest<List<EmployeeVM>>
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }

    public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, List<EmployeeVM>>
    {
        IEmployeeRepository _repository;
        public GetEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public Task<List<EmployeeVM>> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
        {
            var employees = _repository.GetEntities();

            return Task.Run(() => employees.Select(x => new EmployeeVM
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                JoinDate = x.JoinDate,
                DepartementId = x.DepartementId,
                Id = x.Id
            })
            .Skip(query.ItemsPerPage * (query.Page - 1))
            .Take(query.ItemsPerPage)
            .ToList());
        }
    }
}
