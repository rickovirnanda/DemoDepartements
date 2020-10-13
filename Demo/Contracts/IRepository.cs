using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Contracts
{
    public interface IRepository<T> where T:IEntity
    {
        IEnumerable<T> GetEntities();
        IEnumerable<T> GetEntities(Expression<Func<T, bool>> predicate);
        T GetById(long id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }

    public interface IDepartementRepository : IRepository<Departement>
    {

    }

    public interface IEmployeeRepository : IRepository<Employee> { }
}
