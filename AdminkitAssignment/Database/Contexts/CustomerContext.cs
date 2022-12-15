using AdminkitAssignment.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminkitAssignment.Database.Contexts
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerContactPhone> CustomerContactPhones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Customers;Trusted_Connection=True;Trust Server Certificate=True");
        }
    }
}
