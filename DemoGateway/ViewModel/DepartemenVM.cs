using System.Collections.Generic;

namespace DemoGateway.ViewModels
{
    public class CreateDepartementVM
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class DepartementVM:CreateDepartementVM
    {
        public int Id { get; set; }
    }

    public class DepartementDetailVM : DepartementVM
    {
        public List<EmployeeVM> Employees { get; set; }

        public DepartementDetailVM()
        {
            Employees = new List<EmployeeVM>();
        }
    }
}
