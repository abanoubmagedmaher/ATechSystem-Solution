using ATechSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ATechSystem.Repository
{
    public interface IEmployeeRepository
    {
        public void Add(Employee dept);
        public void Update(Employee dept);
        public void Delete(int id);
        public List<Employee> GetAll();
        public Employee GetdeptById(int id);
        public void Save();

    }
}
