using Demo.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Departement:BaseEntity, IEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Location { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Departement()
        {
            Employees = new HashSet<Employee>();
        }
    }
}
