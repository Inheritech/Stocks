using Stocks.Domain.Common;
using Stocks.Domain.Exceptions;
using System;

namespace Stocks.Domain.Aggregates.TransactionAggregate {

    /// <summary>
    /// Stock transaction on an account.
    /// </summary>
    public class Transaction : Entity, IAggregateRoot {
        public int AccountId { get; protected set; }
        public DateTime Timestamp { get; protected set; }
        public Operation Operation { get; protected set; }
        public string Issuer { get; protected set; }
        public int Shares { get; protected set; }
        public int SharePrice { get; protected set; }

        public Transaction(
            DateTime timestamp,
            Operation operation,
            string issuer,
            int shares,
            int sharePrice
        ) {
            static bool IsMarketOpen(TimeSpan time) {
                TimeSpan marketOpens = new TimeSpan(6, 0, 0);
                TimeSpan marketCloses = new TimeSpan(15, 0, 0);

                return time >= marketOpens && time <= marketCloses;
            }

            var timestampTime = timestamp.TimeOfDay;
            if (!IsMarketOpen(timestampTime))
                throw new MarketClosedException();

            if (shares < 1)
                // Here we could probably replace this with some kind of minimum amount of stock for each ticker.
                throw new InvalidTransactionException("The amount of shares for a transaction cannot be less than one.");

            if (sharePrice < 0)
                throw new InvalidTransactionException("The share price cannot be less than one.");

            Timestamp = timestamp;
            Operation = operation;
            Issuer = issuer;
            Shares = shares;
            SharePrice = sharePrice;
        }
    }
}
