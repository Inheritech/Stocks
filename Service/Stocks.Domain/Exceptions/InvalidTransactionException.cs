using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when the values provided to create a transaction violate a business rule.
    /// </summary>
    [Serializable]
    public class InvalidTransactionException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_TRANSACTION";

        public InvalidTransactionException(string description) : base(DefaultCode, description) {
        }

        public InvalidTransactionException(string description, Exception inner) : base(DefaultCode, description, inner) {
        }
    }
}
