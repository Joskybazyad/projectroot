using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositries.Interfacies
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WhithTracking=false);
        Department GetById(int id);
        int Update(Department Entity);
        int Delete(Department Entity);
        int Add(Department Entity);
    }
}
