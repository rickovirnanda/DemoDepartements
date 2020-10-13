using Demo.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Employee : BaseEntity, IEntity
    {
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        public DateTime JoinDate { get; set; }

        [ForeignKey("departement_id")]
        public long DepartementId { get; set; }
        public Departement Departement { get; set; }
    }
}
