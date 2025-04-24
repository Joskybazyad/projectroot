using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DepartmentDetailsDto
    {
        
        //public DepartmentDetailsDto(Department department)
        //{
        //    ID = department.ID;
        //    Name = department.Name;
        //    Code = department.Code;
        //    Description = department.Description;
        //    CreatedBy = 1;
        //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value);
        //    LastModifiedBy = 1;
        //    IsDeleted = department.IsDeleted;
        //}
        public int ID { get; set; } //Pk
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CreatedBy { get; set; } // User ID
        public DateOnly? CreatedOn { get; set; } // Time Of Creation
        public int LastModifiedBy { get; set; } // User ID
        public bool IsDeleted { get; set; }
    }
}
