using ATechSystem.Models;

namespace ATechSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        ATechSystemContext dbcontext;
        public EmployeeRepository(ATechSystemContext _context)
        {
            dbcontext = _context;
        }
        public void Add(Employee dept)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {

            return dbcontext.Employee.ToList();
        }

        public Employee GetdeptById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Employee dept)
        {
            throw new NotImplementedException();
        }
    }
}
