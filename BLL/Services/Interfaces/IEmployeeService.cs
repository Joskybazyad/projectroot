using BLL.DTO.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> SearchEmployeeByName(string name);
        IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking=false);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employee);   
        int UpdateEmployee(UpdatedEmployeeDto employee);   
        bool DeleteEmployee(int id);
    }
}
