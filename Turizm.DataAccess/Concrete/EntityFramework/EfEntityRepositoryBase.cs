using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizm.DataAccess.Abstract;
using System.Data.Entity;
using Turizm.Entities.Abstract;
using System.Linq.Expressions;
using Turizm.Entities.Concrete;

namespace Turizm.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepository<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using(TContext context = new TContext())
            {
                var ent = context.Entry(entity);
                ent.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var ent = context.Entry(entity);
                ent.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            using(TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var ent = context.Entry(entity);
                ent.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
