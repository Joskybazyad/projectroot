using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using projectroot.ViewModels;

namespace projectroot.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,ILogger<DepartmentController> _logger,IWebHostEnvironment _environment) : Controller
    {
        //private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            var departments =_departmentService.GetAllDepartments();
            return View(departments);
        }
        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _departmentService.AddDepartment(departmentDto);
                    if (result > 0) 
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Created !!");
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
            return View(departmentDto);
        }
        #endregion
        #region Details Of Department
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmenById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }
        #endregion
        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)return BadRequest();
            var department = _departmentService.GetDepartmenById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentEditViewModel()
            {
                //Id = department.ID,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn.Value
            };
            return View(departmentViewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id,DepartmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var updatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };
                int result = _departmentService.UpdateDepartment(updatedDepartment);
                if (result > 0)return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can't Be Updated !!");
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
    }
}
