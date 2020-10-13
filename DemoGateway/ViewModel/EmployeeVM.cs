using System;

namespace DemoGateway.ViewModels
{
    public class CreateEmployeeVM 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long DepartementId { get; set; }
        public DateTime JoinDate { get; set; }
    }

    public class EmployeeVM : CreateEmployeeVM
    {
        public long Id { get; set; }
    }

    public class EmployeeDetailVM : EmployeeVM
    {
        public DepartementVM Department { get; set; }
    }
}
