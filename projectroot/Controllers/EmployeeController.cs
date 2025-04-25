using BLL.DTO.DepartmentDto;
using BLL.DTO.EmployeeDto;
using BLL.Services.Classes;
using BLL.Services.Interfaces;
using DAL.Models.EmployeeModel;
using Microsoft.AspNetCore.Mvc;
using projectroot.ViewModels;
using projectroot.ViewModels.Employee;

namespace projectroot.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService, ILogger<EmployeeController> _logger, IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            TempData.Keep();
            var Employees = _employeeService.GetAllEmployees();
            // Binding through view's dictionary : transfeare Data From Action To View
            //1- ViewData
            ViewData["Massage"] = "Hello ViewData";
            string msg01 = ViewData["Message"]as string;
            //2- ViewBag
            ViewBag.Massage = "Hello ViewBag";
            string msg02=ViewBag.Massage;
            return View(Employees);
        }
        #region Create Employee
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeCreatedDto = new CreatedEmployeeDto()
                    {
                        Name = employeeDto.Name,
                        Address = employeeDto.Address,
                        Age = employeeDto.Age,
                        Salary = employeeDto.Salary,
                        Email = employeeDto.Email,
                        IsActive = employeeDto.IsActive,
                        PhoneNumber = employeeDto.PhoneNumber,
                        EmployeeType = employeeDto.EmployeeType,
                        Gender = employeeDto.Gender,
                        HiringDate = employeeDto.HiringDate
                    };
                    int result = _employeeService.CreateEmployee(employeeCreatedDto);
                    //3- TempData
                    if (result > 0)
                    {
                        TempData["Massage"] = "Employee Created Successfuly";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Massage"] = "Employee Creation Faild ";
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Created !!");
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //1- Development => Log Error In Consol And Return Same View With Error Msg
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        //2- Deployment => Error In File | Table In Database And Return Error View
                        _logger.LogError(ex.Message);
                    }

                }
            }
            return View(employeeDto);
        }
        #endregion
        #region Employee Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee =_employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);
        }
        #endregion
        #region Employee Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeDto = new EmployeeViewModel()
            {
                
                Name=employee.Name,
                Address=employee.Address,
                Age=employee.Age,
                Email=employee.Email,
                PhoneNumber=employee.PhoneNumber,
                IsActive=employee.IsActive,
                HiringDate=employee.HiringDate,
                Gender=Enum.Parse<Gender>(employee.Gender),
                EmployeeType=Enum.Parse<EmployeeType>(employee.EmployeeType),
            };
            return View(employeeDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var employeeUpdatedDto = new UpdatedEmployeeDto()
                {
                    Id=id.Value,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Age = viewModel.Age,
                    Salary = viewModel.Salary,
                    Email = viewModel.Email,
                    IsActive = viewModel.IsActive,
                    PhoneNumber = viewModel.PhoneNumber,
                    EmployeeType = viewModel.EmployeeType,
                    Gender = viewModel.Gender,
                    HiringDate = viewModel.HiringDate
                };
                int result = _employeeService.UpdateEmployee(employeeUpdatedDto);
                if (result > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can't Be Updated !!");
                }
            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    //1- Development => Log Error In Consol And Return Same View With Error Msg
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    //2- Deployment => Error In File | Table In Database And Return Error View
                    _logger.LogError(ex.Message);
                }
            }
            return View(viewModel);
        }
        #endregion
        #region Employee Delete
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var Deleted = _employeeService.DeleteEmployee(id);
                if(Deleted)return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    //1- Development => Log Error In Consol And Return Same View With Error Msg
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    //2- Deployment => Error In File | Table In Database And Return Error View
                    _logger.LogError(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
