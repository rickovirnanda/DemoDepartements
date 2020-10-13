using Demo.Models;
using Demo.ViewModels;
using System.Collections.Generic;

namespace Demo.Contracts
{
    public interface IPersonRepo
    {
        List<Person> GetListPerson(int page, int itemsPerPage);
        Person GetPersonById(long id);
        SuccessResponse AddPerson(Person person);
    }

    public interface IDepartementRepo 
    {
        List<DepartementVM> GetDepartments(int page, int itemsPerPage);
        DepartementDetailVM GetDepartmentById(long id);
        SuccessResponse AddDepartement(CreateDepartemenVM department);
        SuccessResponse UpdateDepartement(Departement department);
        SuccessResponse DeleteDepartement(long id);
    }

    public interface IEmployeeRepo
    {
        List<EmployeeVM> GetEmployees (int page, int itemsPerPage);
        EmployeeDetailVM GetEmployeeById (long id);
        SuccessResponse AddEmployee(CreateEmployeeVM employee);
        SuccessResponse UpdateEmployee(Employee employee);
        SuccessResponse DeleteEmployee(long id);
    }
}
