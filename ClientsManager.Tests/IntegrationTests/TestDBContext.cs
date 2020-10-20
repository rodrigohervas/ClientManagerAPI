using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsManager.Tests.IntegrationTests
{
    public class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        {

        }

        //DbSets
        public DbSet<BillableActivity> BillableActivities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<LegalCase> LegalCases { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}