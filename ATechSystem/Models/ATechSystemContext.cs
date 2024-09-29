using Microsoft.EntityFrameworkCore;

namespace ATechSystem.Models
{
    public class ATechSystemContext :DbContext
    {
        public DbSet<Department> Department { get; set; }

        public ATechSystemContext() : base()
        {

        }
        public ATechSystemContext(DbContextOptions<ATechSystemContext> options):base(options)
        {
            
        }
    }
}
