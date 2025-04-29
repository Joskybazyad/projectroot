using BLL.DTO.DepartmentDto;
using BLL.Factories;
using BLL.Services.Interfaces;
using DAL.Data.Repositries.Classes;
using DAL.Data.Repositries.Interfacies;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Classes
{
    public class DepartmentService(IUnitOfWork unitOfWork/*IDepartmentRepository departmentRepository*/) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments =_unitOfWork.DepartmentRepository.GetAll();
            //Manual Mapping
            //var departmentsToReturn = departments.Select(d => new DepartmentDto()
            //{
            //    Id=d.ID, 
            //    Name=d.Name,
            //    Description=d.Description,
            //    Code=d.Code,
            //    DateOfCreation=DateOnly.FromDateTime(d.CreatedOn.Value)
            //});
            //return departmentsToReturn;

            // Extension Method
            return departments.Select(d => d.ToDepartmentDto());
        }
        public DepartmentDetailsDto? GetDepartmenById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return null;
            else
            {
                // This Is Manual Mapping
                //var departmentToReturn = new DepartmentDetailsDto()
                //{
                //    ID = department.ID,
                //    Name = department.Name,
                //    Code = department.Code,
                //    Description = department.Description,
                //    CreatedBy = 1,
                //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),
                //    LastModifiedBy = 1,
                //    IsDeleted=department.IsDeleted
                //};
                //return departmentToReturn;
                // There Is Extension Methods
                return department.ToDepartmentDetailsDto();

                // There Is Auto Mapper
                // There Is Constructor Mapping


            }

        }
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToDepartment();
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
             _unitOfWork.DepartmentRepository.Update(departmentDto.ToDepartment());
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var Department =_unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Delete(Department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;

            }
        }
    }
}
