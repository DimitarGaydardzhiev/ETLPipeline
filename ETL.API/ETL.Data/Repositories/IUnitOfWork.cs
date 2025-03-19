using ETL.Data.Models;

namespace ETL.Data.Repositories
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
