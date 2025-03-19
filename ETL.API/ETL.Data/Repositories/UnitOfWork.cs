using ETL.Data.Models;

namespace ETL.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private EtlContext context;

        private GenericRepository<Transaction> transactionRepository;

        public UnitOfWork(EtlContext context)
        {
            this.context = context;
        }

        public GenericRepository<Transaction> TransactionRepository
        {
            get
            {
                if (transactionRepository == null)
                {
                    this.transactionRepository = new GenericRepository<Transaction>(context);
                }
                return transactionRepository;
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
