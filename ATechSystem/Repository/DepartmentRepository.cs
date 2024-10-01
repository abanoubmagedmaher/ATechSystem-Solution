using ATechSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ATechSystem.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        ATechSystemContext dbcontext;
        public DepartmentRepository(ATechSystemContext _context)
        {
            dbcontext = _context;
        }
        public List<Department> GetAll()
        {

            return dbcontext.Department.ToList();
        }
        public void Add(Department Dept)
        {
            dbcontext.Add(Dept);
        }

        public void Update(Department dept)
        {
            dbcontext.Update(dept);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Department GetdeptById(int id)
        {
            return dbcontext.Department.Find(id);
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }

        public List<Department> GetAllDeptDetails()
        {
            return dbcontext.Department.Include(d => d.Emps).ToList();
        }
    }
}
