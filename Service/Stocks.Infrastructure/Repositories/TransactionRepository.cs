using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Common;
using System;

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
    }
}
