using DemoGateway.ViewModel;
using DemoGateway.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoGateway.Data
{
    public interface IEmployeeService
    {
        SuccessResponse AddEmployee(CreateEmployeeVM newEmployee);
        SuccessResponse UpdateEmployee(EmployeeVM employee);
        List<EmployeeVM> GetEmployees(int page, int itemsPerPage);

        EmployeeDetailVM GetEmployeeById(long id);

        SuccessResponse DeleteEmployee(long id);
    }
}
