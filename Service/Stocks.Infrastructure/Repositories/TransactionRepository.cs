using Microsoft.EntityFrameworkCore;
using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Common;
using System;
using System.Threading.Tasks;

namespace Stocks.Infrastructure.Repositories {
    public class TransactionRepository : ITransactionRepository {
        public IUnitOfWork UnitOfWork => _context;

        private readonly StocksContext _context;

        public TransactionRepository(StocksContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Transaction Add(Transaction transaction) {
            if (transaction.HasIdentity())
                return transaction;

            return _context.Transactions
                .Add(transaction)
                .Entity;
        }

        public async Task<Transaction> FindDuplicateOnTimeSpanAsync(Transaction like, TimeSpan range) {
            return await _context.Transactions
                .FirstOrDefaultAsync(other =>
                    other.AccountId == like.AccountId
                    && other.Issuer == like.Issuer
                    && other.Shares == like.Shares
                    && other.OperationId == like.OperationId
                    && Math.Abs(EF.Functions.DateDiffSecond(other.Timestamp, like.Timestamp)) <= range.Seconds
                );
        }
    }
}
