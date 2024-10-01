using System.ComponentModel.DataAnnotations.Schema;

namespace ATechSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public List<Department>? Department { get; set; }
    }
}
