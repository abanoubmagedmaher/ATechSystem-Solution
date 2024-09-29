using ATechSystem.Models;

namespace ATechSystem.Repository
{
    public interface IDepartmentRepository
    {
        public void Add(Department dept);
        public void Update(Department dept);
        public void Delete(int id);
        public List<Department> GetAll();
        public Department GetdeptById(int id);
        public void Save();
       
    }
}
