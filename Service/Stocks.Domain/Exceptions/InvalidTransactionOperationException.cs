using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when a specified exchange type is invalid.
    /// </summary>
    [Serializable]
    public class InvalidTransactionOperationException : DomainException {

        /// <summary>
        /// Business code violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_TRANSACTION_OPERATION";

        /// <summary>
        /// Format for the default human-readable description for this kind of exception.
        /// </summary>
        private const string DefaultDescriptionFormat = "'{0}' is not a valid transaction operation.";

        public InvalidTransactionOperationException(string attemptedOperation) 
            : base(DefaultCode, string.Format(DefaultDescriptionFormat, attemptedOperation)) {
        }

        public InvalidTransactionOperationException(string attemptedOperation, Exception inner) 
            : base(DefaultCode, string.Format(DefaultDescriptionFormat, attemptedOperation), inner) {
        }
    }
}
