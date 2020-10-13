using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo
{
    public class DemoContext : DbContext
    {
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DemoContext(DbContextOptions<DemoContext> dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Departement>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Employee>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}