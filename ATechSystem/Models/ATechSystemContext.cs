using Microsoft.EntityFrameworkCore;

namespace ATechSystem.Models
{
    public class ATechSystemContext :DbContext
    {
        DbSet<Department> Department { get; set; }

        public ATechSystemContext(DbContextOptions<ATechSystemContext> options):base(options)
        {
            
        }
    }
}
