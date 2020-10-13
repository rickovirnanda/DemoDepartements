using System.Collections.Generic;

namespace Demo.ViewModels
{
    public class CreateDepartemenVM
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class DepartementVM : CreateDepartemenVM
    {
        public long Id { get; set; }
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
