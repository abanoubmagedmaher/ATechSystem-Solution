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

       
        #endregion

    }
}
