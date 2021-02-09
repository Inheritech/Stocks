using Stocks.Domain.Common;
using System.Threading.Tasks;

namespace Stocks.Domain.Aggregates.AccountAggregate {
    public interface IAccountRepository : IRepository<Account> {
        Account Add(Account account);
        Account Update(Account account);
        Task<Account> FindAsync(int id);
    }
}
