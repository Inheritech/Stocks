using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;
using System;

namespace Stocks.Domain.Aggregates.AccountAggregate {
    
    /// <summary>
    /// Stock balance of an account.
    /// </summary>
    public class StockBalance : Entity {

        /// <summary>
        /// Identifier of the account owner of this balance.
        /// </summary>
        public int AccountId { get; protected set; }

        /// <summary>
        /// Ticker of the share issuing entity for the shares of this balance.
        /// </summary>
        public string Issuer { get; protected set; }

        /// <summary>
        /// Total amount of shares.
        /// </summary>
        public int Shares { get; protected set; }

        /// <summary>
        /// Average price of all shares.
        /// </summary>
        public decimal SharePrice { get; protected set; }

        protected StockBalance() {
        }

        public StockBalance(
            Account account,
            string issuer
        ) {
            if (account == null)
                throw new InvalidStockBalanceException("A stock balance requires a related existing account.");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new InvalidStockBalanceException("The stock balance requires a valid non-empty issuer.");

            AccountId = account.Id;
            Issuer = issuer;
            Shares = 0;
            SharePrice = 0;
        }

        /// <summary>
        /// Add shares to this balance and update the average price based on the share price.
        /// </summary>
        /// <param name="amount">Amount of shares to add.</param>
        /// <param name="price">Price of each share.</param>
        public void AddShares(int amount, decimal price) {
            // Could possibly extract this code but it might generate a dangling/unused dependency in the future
            decimal WeightedAverage(int size, decimal average, int totalSize)
                => (decimal)size / totalSize * average;
            decimal SumAveragesByWeight(int aSize, decimal aAverage, int bSize, decimal bAverage) {
                int totalSize = aSize + bSize;
                return WeightedAverage(aSize, aAverage, totalSize) + WeightedAverage(bSize, bAverage, totalSize);
            }

            if (amount < 1)
                throw new InvalidStockBalanceOperationException("At least one share is needed to be added into the balance.");

            if (price <= 0)
                throw new InvalidStockBalanceOperationException("The share price must be more than zero to be added into the balance.");

            SharePrice = Math.Round(
                SumAveragesByWeight(
                    Shares, SharePrice,
                    amount, price
                ),
                4,
                MidpointRounding.ToEven
            );
            Shares += amount;
        }

        /// <summary>
        /// Remove shares from this balance
        /// </summary>
        /// <param name="amount">Amount of shares to remove</param>
        public void SubtractShares(int amount) {
            if (amount < 1)
                throw new InvalidStockBalanceOperationException("Cannot remove less than one share from the balance.");

            if (Shares < amount)
                throw new InsufficientStockBalanceException();

            Shares -= amount;
        }

        /// <summary>
        /// Determine if the balance is zero
        /// </summary>
        public bool IsEmpty() => Shares == 0;
    }
}
