using ATechSystem.DTOS;
using ATechSystem.Models;
using ATechSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATechSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository _EmployeeRepo;
        public EmployeeController(IEmployeeRepository empRepo)
        {
            _EmployeeRepo = empRepo;
        }
        #region Get Methods 

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var EmpLst = _EmployeeRepo.GetAll().ToList();
            return Ok(EmpLst);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public ActionResult<GenralResponseDTO> GetEmployeeById(int id) 
        {
            var emp = _EmployeeRepo.GetEmpById(id);
            var genralResponse = new GenralResponseDTO();
         
            if (emp != null)
            {
                genralResponse.IsSuccess = true;
                genralResponse.Data = emp;
            }
            else
            {
                genralResponse.IsSuccess = false;
                genralResponse.Data = "Id Is Invalid !";

            }
            return genralResponse;
        }


        #endregion

        #region Add
        [HttpPost]
        public IActionResult AddEmp(Employee emp)
        {
            if (ModelState.IsValid)
            {
                // Handel Logic With Custom Validatin and Must
                // add Validation Into Class 
            }
            return Ok(emp);
        }
        #endregion

    }
}
