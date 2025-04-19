using DAL.Data.Repositries.Interfacies;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositries.Classes
{
    // Primary Constructor
    public class DepartmentRepository(AppDBContext _dbcontext) : IDepartmentRepository
    {
        private readonly AppDBContext dbcontext = _dbcontext;

        public int Add(Department Entity)
        {
            dbcontext.Add(Entity);
            return dbcontext.SaveChanges();
        }

        public int Delete(Department Entity)
        {
            dbcontext.Departments.Remove(Entity);
            return dbcontext.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return dbcontext.Departments.ToList();
            }
            else
            {
                return dbcontext.Departments.AsNoTracking().ToList();
            }
        }

        public Department GetById(int id)
        {
            return dbcontext.Departments.Find(id);
        }

        public int Update(Department Entity)
        {
             dbcontext.Departments.Update(Entity);
            return dbcontext.SaveChanges();
        }
    }
}
