using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ATechSystem.Models
{
    public class ATechSystemContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public ATechSystemContext() : base()
        {

        }
        public ATechSystemContext(DbContextOptions<ATechSystemContext> options):base(options)
        {
            
        }
    }
}
