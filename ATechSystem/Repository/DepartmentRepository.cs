using ATechSystem.Models;

namespace ATechSystem.Repository
{
    public class DepartmentRepository :IDepartmentRepository
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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Department GetdeptById(int id)
        {
            return dbcontext.Department.FirstOrDefault(d => d.Id == id);
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }
    }
}
