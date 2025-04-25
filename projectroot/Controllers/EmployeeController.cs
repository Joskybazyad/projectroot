using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace projectroot.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }
    }
}
