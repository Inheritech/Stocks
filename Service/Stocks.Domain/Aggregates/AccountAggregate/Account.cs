using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.Domain.Aggregates.AccountAggregate {

    /// <summary>
    /// Stock account with a cash balance.
    /// </summary>
    public class Account : Entity, IAggregateRoot {

        /// <summary>
        /// Current available cash of the account.
        /// </summary>
        public decimal Cash { get; protected set; }

        /// <summary>
        /// Current share balances for this account
        /// </summary>
        public IReadOnlyCollection<StockBalance> StockBalances => _stockBalances;

        // This works for a small set of balances, if hundreds of balances need to be handled it would be best
        // to handle share balances as separate related aggregates instead of part of the account aggregate
        private readonly List<StockBalance> _stockBalances;

        protected Account() {
            _stockBalances = new List<StockBalance>();
        }

        /// <summary>
        /// Create a new account with the specified initial balance.
        /// </summary>
        /// <param name="initialCash">Initial balance amount for the account.</param>
        /// <exception cref="InvalidInitialBalanceException">Thrown if the initial balance of the account is less than zero.</exception>
        public Account(decimal initialCash) : this() {
            if (initialCash < 0)
                throw new InvalidInitialBalanceException();

            Cash = initialCash;
        }

        /// <summary>
        /// Increase the balance of the account by the specified amount.
        /// </summary>
        /// <param name="amount">Amount to deposit.</param>
        public void Deposit(decimal amount) {
            if (amount < 0)
                throw new InvalidAccountOperationException("The amount to deposit cannot be negative.");
            Cash += amount;
        }

        /// <summary>
        /// Deduct the specified amount from the account.
        /// </summary>
        /// <param name="amount">Amount to deduct.</param>
        /// <exception cref="InsufficientBalanceException">Thrown if the current balance is not enough to cover the deduction.</exception>
        public void Deduct(decimal amount) {
            if (amount < 0)
                throw new InvalidAccountOperationException("The amount to deduct must be positive to be deducted.");
            if (Cash < amount)
                throw new InsufficientBalanceException();
            Cash -= amount;
        }

        /// <summary>
        /// Execute stock order.
        /// </summary>
        public Transaction PlaceOrder(DateTime timestamp, Operation operation, string issuer, int shares, decimal sharePrice) {
            var transaction = new Transaction(this, timestamp, operation, issuer, shares, sharePrice);
            var balance = _stockBalances.FirstOrDefault(_ => _.Issuer == issuer);

            if (balance is null) {
                balance = new StockBalance(this, issuer);
                _stockBalances.Add(balance);
            }

            if (operation == Operation.Buy) {
                balance.AddShares(shares, sharePrice);
                Deduct(transaction.GetTotalPrice());
            } else {
                balance.SubtractShares(transaction.Shares);
                Deposit(transaction.GetTotalPrice());
            }

            if (balance.IsEmpty())
                _stockBalances.Remove(balance);

            return transaction;
        }
    }
}
