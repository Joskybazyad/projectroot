using DAL.Data.Repositries.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositries.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IDepartmentRepository> _departmentRepository;
        private readonly AppDBContext dbcontext;
        private Lazy<IEmployeeRepository> _employeeRepository;
        public UnitOfWork(AppDBContext _dbcontext)
        {
            _departmentRepository=new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(_dbcontext));
            dbcontext = _dbcontext;
            _employeeRepository =new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(_dbcontext));
        }

        public IEmployeeRepository EmployeeRepository { get { return _employeeRepository.Value; }  }
        public IDepartmentRepository DepartmentRepository { get { return _departmentRepository.Value; }  }

        public int SaveChanges()
        {
            return dbcontext.SaveChanges();
        }
    }
}
