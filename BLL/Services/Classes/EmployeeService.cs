using AutoMapper;
using BLL.DTO.EmployeeDto;
using BLL.Services.Interfaces;
using DAL.Data.Repositries.Interfacies;
using DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
        public int CreateEmployee(CreatedEmployeeDto employee)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employee);
            return _employeeRepository.Add(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee)>0?true:false;
            }
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking)
        {
            var Employees = _employeeRepository.GetAll(withTracking);
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
            var Employee = _employeeRepository.GetById(id);
            //if(Employee == null)return null;
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
            //        LastModifiedBy=Employee.LastModifiedBy,

            //    };
            //    return returnedEmp;
            //}
            return Employee == null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employee)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employee));
        }
    }
}
