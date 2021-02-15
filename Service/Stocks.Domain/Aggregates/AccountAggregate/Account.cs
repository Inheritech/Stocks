using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;

namespace Stocks.Domain.Aggregates.AccountAggregate {

    /// <summary>
    /// Stock account with a cash balance.
    /// </summary>
    public class Account : Entity, IAggregateRoot {

        /// <summary>
        /// Current available cash of the account.
        /// </summary>
        public int Cash { get; protected set; }

        protected Account() {
        }

        /// <summary>
        /// Create a new account with the specified initial balance.
        /// </summary>
        /// <param name="initialCash">Initial balance amount for the account.</param>
        /// <exception cref="InvalidInitialBalanceException">Thrown if the initial balance of the account is less than zero.</exception>
        public Account(int initialCash) : this() {
            if (initialCash < 0)
                throw new InvalidInitialBalanceException();

            Cash = initialCash;
        }

        /// <summary>
        /// Increase the balance of the account by the specified amount.
        /// </summary>
        /// <param name="amount">Amount to deposit.</param>
        public void Deposit(int amount) {
            if (amount < 0)
                throw new InvalidAccountOperationException("The amount to deposit cannot be negative.");
            Cash += amount;
        }

        /// <summary>
        /// Deduct the specified amount from the account.
        /// </summary>
        /// <param name="amount">Amount to deduct.</param>
        /// <exception cref="InsufficientBalanceException">Thrown if the current balance is not enough to cover the deduction.</exception>
        public void Deduct(int amount) {
            if (amount < 0)
                throw new InvalidAccountOperationException("The amount to deduct must be positive to be deducted.");
            if (Cash < amount)
                throw new InsufficientBalanceException();
            Cash -= amount;
        }
    }
}
