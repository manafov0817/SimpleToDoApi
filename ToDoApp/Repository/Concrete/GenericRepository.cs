using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.WebApi.Repository.Abstract;

namespace ToDoApp.WebApi.Repository.Concrete
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()

    {
        public ICollection<TEntity> GetAll()
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }
        public bool Create(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                return context.SaveChanges() >= 0 ? true : false;
            }
        }

        public bool Update(TEntity entity)
        {
            using (var context = new TContext())
            {
 
                context.Set<TEntity>().Update(entity);

                return context.SaveChanges() >= 0 ? true : false;

            }
        }

        public bool Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                return context.SaveChanges() >= 0 ? true : false;
            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }
    }
}
