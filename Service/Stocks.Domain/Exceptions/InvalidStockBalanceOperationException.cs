using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when an attempted operation on a share balance is not valid for the current share balance state.
    /// </summary>
    [Serializable]
    public class InvalidStockBalanceOperationException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_STOCK_BALANCE_OPERATION";

        public InvalidStockBalanceOperationException(string description) : base(DefaultCode, description) {
        }

        public InvalidStockBalanceOperationException(string description, Exception inner) : base(DefaultCode, description, inner) {
        }
    }
}
