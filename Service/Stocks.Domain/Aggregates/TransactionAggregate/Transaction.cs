using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;
using System;

namespace Stocks.Domain.Aggregates.TransactionAggregate {

    /// <summary>
    /// Stock transaction on an account.
    /// </summary>
    public class Transaction : Entity, IAggregateRoot {

        /// <summary>
        /// Identifier of the account that made this transaction.
        /// </summary>
        public int AccountId { get; protected set; }

        /// <summary>
        /// Date and time at which the transaction occured.
        /// </summary>
        public DateTime Timestamp { get; protected set; }

        /// <summary>
        /// Operation handled by the transaction.
        /// </summary>
        public Operation Operation { get; protected set; }

        /// <summary>
        /// Ticker of the share issuing entity for the shares of this transaction.
        /// </summary>
        public string Issuer { get; protected set; }

        /// <summary>
        /// Amount of shares in this transaction.
        /// </summary>
        public int Shares { get; protected set; }

        /// <summary>
        /// Price of each share at the <see cref="Timestamp"/> of the transaction.
        /// </summary>
        public decimal SharePrice { get; protected set; }

        protected Transaction() {

        }

        /// <summary>
        /// Create a new transaction for the provided account.
        /// </summary>
        /// <param name="timestamp">Time of the transaction. Note: It must happen while the market is open.</param>
        /// <param name="operation">Operation of the transaction.</param>
        /// <param name="issuer">Ticker of the issuer for the related shares.</param>
        /// <param name="shares">Amount of shares to handle in this transaction.</param>
        /// <param name="sharePrice">Price at which to operate the shares for this transaction.</param>
        public Transaction(
            Account account,
            DateTime timestamp,
            Operation operation,
            string issuer,
            int shares,
            decimal sharePrice
        ) {
            static bool IsMarketOpen(TimeSpan time) {
                TimeSpan marketOpens = new TimeSpan(6, 0, 0);
                TimeSpan marketCloses = new TimeSpan(15, 0, 0);

                return time >= marketOpens && time <= marketCloses;
            }

            if (account == null)
                throw new InvalidTransactionException("The transaction requires an existing account to be related to.");
            
            if (string.IsNullOrWhiteSpace(issuer))
                throw new InvalidTransactionException("The transaction requires a valid non-empty issuer.");

            var timestampTime = timestamp.TimeOfDay;
            if (!IsMarketOpen(timestampTime))
                throw new MarketClosedException();

            if (shares < 1)
                // Here we could probably replace this with some kind of minimum amount of stock for each ticker.
                throw new InvalidTransactionException("The amount of shares for a transaction cannot be less than one.");

            if (sharePrice < 1)
                throw new InvalidTransactionException("The share price cannot be less than one.");

            AccountId = account.Id;
            Timestamp = timestamp;
            Operation = operation ?? throw new InvalidTransactionException("The transaction requires a valid operation.");
            Issuer = issuer;
            Shares = shares;
            SharePrice = sharePrice;
        }

        /// <summary>
        /// Get the total price of this transaction.
        /// </summary>
        public decimal GetTotalPrice() => Shares * SharePrice;
    }
}
