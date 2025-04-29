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
    public class GenericRepository<TEntity>(AppDBContext _dbcontext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDBContext dbcontext = _dbcontext;

        public void Add(TEntity Entity)
        {
            dbcontext.Set<TEntity>().Add(Entity);
            //dbcontext.Add(Entity);
            //return dbcontext.SaveChanges();
        }

        public void Delete(TEntity Entity)
        {
            dbcontext.Set<TEntity>().Remove(Entity);
            //return dbcontext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return dbcontext.Set<TEntity>().Where(E=>E.IsDeleted!=true).ToList();
            }
            else
            {
                return dbcontext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
            }
        }

        public TEntity GetById(int id)
        {
            return dbcontext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity Entity)
        {
            dbcontext.Set<TEntity>().Update(Entity);
            //return dbcontext.SaveChanges();
        }
    }
}
