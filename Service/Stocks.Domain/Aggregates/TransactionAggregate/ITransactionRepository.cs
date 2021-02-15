using Stocks.Domain.Common;

namespace Stocks.Domain.Aggregates.TransactionAggregate {
    public interface ITransactionRepository : IRepository<Transaction> {
        Transaction Add(Transaction transaction);
    }
}
