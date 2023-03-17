using employeehub_api.Models;
using Microsoft.EntityFrameworkCore;

namespace employeehub_api.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }


    }
}
