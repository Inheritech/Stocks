using System.Collections.Generic;
using System.Linq;

namespace Stocks.API.Commands.Results {
    public class PlaceOrderResult : UseCaseResult {
        public class AccountOverview {
            public class StockBalanceOverview {
                public string Issuer { get; }
                public int Shares { get; }
                public decimal SharePrice { get; }

                public StockBalanceOverview(string issuer, int shares, decimal sharePrice) {
                    Issuer = issuer;
                    Shares = shares;
                    SharePrice = sharePrice;
                }
            }

            public decimal Cash { get; }
            public IReadOnlyList<StockBalanceOverview> StockBalances { get; }

            public AccountOverview(decimal cash, IEnumerable<StockBalanceOverview> balances) {
                Cash = cash;
                StockBalances = balances.ToList();
            }
        }

        public AccountOverview Account { get; }

        public PlaceOrderResult(AccountOverview account) {
            Account = account;
        }
    }
}
