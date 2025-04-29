using DAL.Data.Repositries.Interfacies;
using DAL.Models;
using DAL.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositries.Classes
{
    public class EmployeeRepository(AppDBContext _dbcontext) : GenericRepository<Employee>(_dbcontext), IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployeeByName(string name)
        {
            return _dbcontext.Employees.Where(emp => emp.Name.ToLower().Contains(name));
        }
    }
}
