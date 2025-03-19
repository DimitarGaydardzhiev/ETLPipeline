using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ETL.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        internal EtlContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(EtlContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get(
            out int totalCount,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = 0)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            totalCount = query.Count();

            query = query.Skip(skip).Take(take > 0 ? take : int.MaxValue);

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            var entity = dbSet.Find(id);
            return entity;
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = GetByID(id);
            if (entityToDelete == null)
                throw new ArgumentNullException();

            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
