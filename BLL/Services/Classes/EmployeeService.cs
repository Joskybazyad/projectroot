using AutoMapper;
using BLL.DTO.EmployeeDto;
using BLL.Services.AttachmentService;
using BLL.Services.Interfaces;
using DAL.Data.Repositries.Interfacies;
using DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal; // For HeaderDictionary (in older ASP.NET Core versions)
using System.IO;
using BLL.Profiles;


namespace BLL.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper,IAttachmentService _attachmentService) : IEmployeeService
    {
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null) 
            { 
                Employee.ImageName=_attachmentService.Upload(employeeDto.Image,"Images");
            }
           _unitOfWork.EmployeeRepository.Add(Employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                var imgInfo = employee.ImageName;
                employee.ImageName = null;
                _unitOfWork.EmployeeRepository.Update(employee);
                var result = _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    _attachmentService.Delete(imgInfo, "Images");
                    return true;
                }
                else
                    return false;
            }
        }
        public IEnumerable<EmployeeDto> SearchEmployeeByName(string name)
        {
            var Employees= _unitOfWork.EmployeeRepository.GetEmployeeByName(name.ToLower());
            //var returnedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            var returnedEmployees = Employees.Select(emp => new EmployeeDto()
            {
                Id = emp.ID,
                Name = emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                Salary = emp.Salary,
                IsActive = emp.IsActive,
                EmployeeType = emp.EmployeeType.ToString(),
                Gender = emp.Gender.ToString(),
            });

            return returnedEmployees;


        }
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var Employees = _unitOfWork.EmployeeRepository.GetAll(withTracking);
            var returnedEmployees=_mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);//AutoMapper
            //var returnedEmployees = Employees.Select(emp => new EmployeeDto()
            //{
            //    Id=emp.ID, 
            //    Name=emp.Name,
            //    Age=emp.Age,
            //    Email=emp.Email,
            //    Salary=emp.Salary,
            //    IsActive=emp.IsActive,
            //    EmployeeType=emp.EmployeeType.ToString(),
            //    Gender=emp.Gender.ToString(),
            //}) ;
            return returnedEmployees;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            //if (Employee == null) return null;
            //else
            //{
            //    var returnedEmp = new EmployeeDetailsDto()
            //    {
            //        Id = Employee.ID,
            //        Name = Employee.Name,
            //        Age = Employee.Age,
            //        Email = Employee.Email,
            //        Salary = Employee.Salary,
            //        IsActive = Employee.IsActive,
            //        EmployeeType = Employee.EmployeeType.ToString(),
            //        Gender = Employee.Gender.ToString(),
            //        PhoneNumber = Employee.PhoneNumber,
            //        HiringDate = DateOnly.FromDateTime(Employee.HiringDate),
            //        //CreatedOn = Employee.CreatedOn.Value,
            //        CreatedBy = Employee.CreatedBy,
            //        LastModifiedBy = Employee.LastModifiedBy,


            //    };
            //    return returnedEmp;
            //}
            return Employee == null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(Employee);
            
        }

        public int UpdateEmployee(UpdatedEmployeeDto employee)
        {
            // _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employee));
            //return _unitOfWork.SaveChanges();
            var currentEmp = _unitOfWork.EmployeeRepository.GetById(employee.Id);
            if (employee.Image is not null)
            {
                
                if (currentEmp.ImageName is not null)
                {
                    
                    _attachmentService.Delete(currentEmp.ImageName, "Images");
                    currentEmp.ImageName = _attachmentService.Upload(employee.Image, "Images");
                    
                }
                else
                {
                    currentEmp.ImageName = _attachmentService.Upload(employee.Image, "Images");
                }


            }
            else
            {
                _attachmentService.Delete(currentEmp.ImageName, "Images");
            }
            _unitOfWork.EmployeeRepository.Update(currentEmp);
            return _unitOfWork.SaveChanges();
        }
    }
}
