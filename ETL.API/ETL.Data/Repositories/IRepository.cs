using System.Linq.Expressions;

namespace ETL.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Get(
                    out int totalCount,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = 0);

        TEntity GetByID(object id);

        void Add(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
