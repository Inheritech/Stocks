using Microsoft.EntityFrameworkCore;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Common;
using System;
using System.Threading.Tasks;

namespace Stocks.Infrastructure.Repositories {
    public class AccountRepository : IAccountRepository {
        public IUnitOfWork UnitOfWork => _context;

        private readonly StocksContext _context;

        public AccountRepository(StocksContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Account Add(Account account) {
            if (account.HasIdentity())
                return account;

            return _context.Accounts
                .Add(account)
                .Entity;
        }

        public async Task<Account> FindAsync(int id) {
            var account = await _context.Accounts
                .SingleOrDefaultAsync(_ => _.Id == id);

            if (account != null) {
                await _context.Entry(account)
                    .Collection(_ => _.StockBalances).LoadAsync();
            }

            return account;
        }

        public Account Update(Account account) {
            return _context.Accounts
                .Update(account)
                .Entity;
        }
    }
}
