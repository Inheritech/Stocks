using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when the values provided to create a share balance violate a business rule.
    /// </summary>
    [Serializable]
    public class InvalidStockBalanceException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_STOCK_BALANCE";

        public InvalidStockBalanceException(string description) : base(DefaultCode, description) {
        }

        public InvalidStockBalanceException(string description, Exception inner) : base(DefaultCode, description, inner) {
        }
    }
}
