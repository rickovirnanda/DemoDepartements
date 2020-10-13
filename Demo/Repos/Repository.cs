using Demo.Contracts;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Demo.Repos
{
    public class DepartementRepository : BaseRepository<Departement>, IDepartementRepository
    {
        public DepartementRepository(DemoContext context) : base(context) { }
        public override Departement GetById(long id)
        {
            return Context.Set<Departement>().Include(x => x.Employees).SingleOrDefault(x => x.Id == id);
        }
    }

    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DemoContext context) : base(context) { }
    }
}