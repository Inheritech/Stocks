using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when the stock balance of an accout is not enough to cover a sale.
    /// </summary>
    [Serializable]
    public class InsufficientStockBalanceException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INSUFFICIENT_STOCK";

        /// <summary>
        /// Human-readable description to show for this kind of exception.
        /// </summary>
        private const string DefaultDescription = "The current stock balance is not enough to cover the sale.";

        public InsufficientStockBalanceException() : base(DefaultCode, DefaultDescription) {
        }

        public InsufficientStockBalanceException(Exception inner) : base(DefaultCode, DefaultDescription, inner) {
        }
    }
}
