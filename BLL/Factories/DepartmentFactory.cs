using BLL.DTO.DepartmentDto;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Factories
{
    static public class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department d)
        {
            return new DepartmentDto()
            {
                Id = d.ID,
                Name = d.Name,
                Description = d.Description,
                Code = d.Code,
                DateOfCreation = DateOnly.FromDateTime(d.CreatedOn.Value)
            };
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                ID = department.ID,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = 1,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),
                LastModifiedBy = 1,
                IsDeleted = department.IsDeleted
            };
        }
        public static Department ToDepartment(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto?.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
        public static Department ToDepartment(this UpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                ID=departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto?.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
