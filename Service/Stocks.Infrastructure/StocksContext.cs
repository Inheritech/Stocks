using Microsoft.EntityFrameworkCore;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Common;
using Stocks.Infrastructure.EntityConfigurations;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks.Infrastructure {
    public class StocksContext : DbContext, IUnitOfWork {

        public const string DefaultSchema = nameof(Stocks);

        public DbSet<Account> Accounts { get; set; }
        public DbSet<StockBalance> StockBalances { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Operation> OperationValues { get; set; }

        public StocksContext(DbContextOptions<StocksContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration(DefaultSchema));
            modelBuilder.ApplyConfiguration(new StockBalanceEntityConfiguration(DefaultSchema));
            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration(DefaultSchema));
            modelBuilder.ApplyConfiguration(new OperationEntityConfiguration(DefaultSchema));
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
