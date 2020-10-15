using DemoGateway.Contracts;
using DemoGateway.Data;
using DemoGateway.ViewModel;
using DemoGateway.ViewModels;
using Google.Protobuf.WellKnownTypes;
using ServiceProto.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGateway.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly ProtoEmployee.ProtoEmployeeClient _employee;
        public EmployeeService(IGrpcClient client)
        {
            _employee = new ProtoEmployee.ProtoEmployeeClient(client.DemoChannel);
        }
        public SuccessResponse AddEmployee(CreateEmployeeVM newEmployee)
        {
            var result = _employee.AddEmployee(new AddEmployeeMessage
            {
                DepartementId = newEmployee.DepartementId,
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                JoinDate = Timestamp.FromDateTime(newEmployee.JoinDate.ToUniversalTime())
            });

            return new SuccessResponse
            {
                Success = result.Success,
                Reason = result.Reason
            };
        }

        public SuccessResponse DeleteEmployee(long id)
        {
            throw new NotImplementedException();
        }

        public EmployeeDetailVM GetEmployeeById(long id)
        {
            var result = _employee.GetEmployeeById(new GetByIdRequest { Id = id });

            var employee = new EmployeeDetailVM
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                JoinDate = result.JoinDate.ToDateTime(),
                Department = new DepartementVM
                {
                    Id = (int)result.Departement.Id,
                    Location = result.Departement.Location,
                    Name = result.Departement.Location
                }
            };

            return employee;
        }

        public List<EmployeeVM> GetEmployees(int page, int itemsPerPage)
        {
            var result = _employee.GetEmployees(new GetEmployeeMessage { Page = page, ItemsPerPage = itemsPerPage });

            var employees = result.Employees.Select(x => new EmployeeVM
            {
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName,
                JoinDate = x.JoinDate.ToDateTime(),
                DepartementId = x.DepartementId
            }).ToList();

            return employees;
        }
    }
}
