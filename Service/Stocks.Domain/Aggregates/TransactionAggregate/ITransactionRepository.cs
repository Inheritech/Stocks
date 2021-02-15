using Stocks.Domain.Common;
using System;
using System.Threading.Tasks;

namespace Stocks.Domain.Aggregates.TransactionAggregate {
    public interface ITransactionRepository : IRepository<Transaction> {
        Transaction Add(Transaction transaction);
        /// <summary>
        /// Find a transaction with the same values as another in a specific time span from the timestamp.
        /// </summary>
        /// <param name="like">Transaction to compare to.</param>
        /// <param name="range">Time range to search on.</param>
        Task<Transaction> FindDuplicateOnTimeSpanAsync(Transaction like, TimeSpan range);
    }
}
