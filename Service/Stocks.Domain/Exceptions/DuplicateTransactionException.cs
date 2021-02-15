using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when a transaction is too similar to another one
    /// </summary>
    [Serializable]
    public class DuplicateTransactionException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "DUPLICATE_TRANSACTION";

        /// <summary>
        /// Human-readable description for this kind of exception.
        /// </summary>
        private const string DefaultDescription = "The attempted transaction has been deemed to be a duplicate.";

        public DuplicateTransactionException() : base(DefaultCode, DefaultDescription) {
        }

        public DuplicateTransactionException(Exception inner) : base(DefaultCode, DefaultDescription, inner) {
        }
    }
}
