using ATechSystem.DTOS;
using ATechSystem.Models;
using ATechSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATechSystem.Controllers
{
    [Route("api/[controller]")] // api/Department
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentRepository _departmentRepo;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepo = departmentRepository;
        }

        #region Get Methods 
        [HttpGet] // api/Department => Get
        public IActionResult GetAllDepartment()
        {
            var deptLst = _departmentRepo.GetAll().ToList();
            return Ok(deptLst);
        }

        [HttpGet("{id:int}")] // api/Department/id => Get
        public IActionResult GetDepartment(int id)
        {
            var dept = _departmentRepo.GetdeptById(id);
            return Ok(dept);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetDeptByName(string name)
        {
            var dept = _departmentRepo.GetAll().Where(d => d.Name.ToLower() == name.Trim().ToLower()).FirstOrDefault();
            return Ok(dept);
        }

        [HttpGet("Count")]
        public ActionResult<List<Department>> GetDepartmentDetails()
        {
            List<Department> deptList = _departmentRepo.GetAllDeptDetails();
            List<DeptWithEmpCountDTO> deptLstDto = new List<DeptWithEmpCountDTO>();
            foreach (var item in deptList)
            {
                DeptWithEmpCountDTO deptDto = new DeptWithEmpCountDTO();
                deptDto.ID = item.Id;
                deptDto.Name = item.Name;
                deptDto.EmpCount = item.Emps.Count();
                deptLstDto.Add(deptDto);
            }
            return Ok(deptLstDto);
        } 
        #endregion

        #region Add
        [HttpPost] // api/Department => POST
        public IActionResult AddDept(Department dept)
        {
            _departmentRepo.Add(dept);
            _departmentRepo.Save();
            return Ok(dept);
        }
        #endregion

        #region Update
        [HttpPut("UpdateDept/{id:int}")] // api/Department => PUT
        public IActionResult UpdateDept(int id,Department dept)
        {
            var deptFromDB= _departmentRepo.GetdeptById(id);
            if (deptFromDB != null)
            {
                deptFromDB.Name = dept.Name;
                deptFromDB.MangerName = dept.MangerName;
                //_departmentRepo.Update(dept);
                _departmentRepo.Save();
                return Ok(dept);
            }
            else
            {
                return NotFound("Department Not Found");
            }
        
        }
        #endregion

        #region Delete
        //[HttpDelete] // api/Department?id=1 => Delete
        //public IActionResult DeleteDept(int id)
        //{

        //}

        #endregion
    }
}
