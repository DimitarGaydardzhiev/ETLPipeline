using ETL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.Data
{
    public class EtlContext : DbContext
    {
        protected readonly DbContextOptions dbContextOptions;

        public EtlContext(DbContextOptions options)
            : base(options)
        {
            this.dbContextOptions = options;
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
        }
    }
}
