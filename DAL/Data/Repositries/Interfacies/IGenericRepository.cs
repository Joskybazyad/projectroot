using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repositries.Interfacies
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll(bool WhithTracking = false);
        TEntity GetById(int id);
        void Update(TEntity Entity);
        void Delete(TEntity Entity);
        void Add(TEntity Entity);
    }
}
