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
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeService
    {
        public int CreateEmployee(CreatedEmployeeDto employee)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employee);
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
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges()>0?true:false;
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
             _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employee));
            return _unitOfWork.SaveChanges();
        }
    }
}
