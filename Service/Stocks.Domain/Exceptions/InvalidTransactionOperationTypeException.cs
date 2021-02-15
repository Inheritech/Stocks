using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when a specified exchange type is invalid.
    /// </summary>
    [Serializable]
    public class InvalidTransactionOperationTypeException : DomainException {

        /// <summary>
        /// Business code violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_TRANSACTION_OPERATION_TYPE";

        /// <summary>
        /// Format for the default human-readable description for this kind of exception.
        /// </summary>
        private const string DefaultDescriptionFormat = "'{0}' is not a valid transaction operation.";

        public InvalidTransactionOperationTypeException(string attemptedOperation) 
            : base(DefaultCode, string.Format(DefaultDescriptionFormat, attemptedOperation)) {
        }

        public InvalidTransactionOperationTypeException(string attemptedOperation, Exception inner) 
            : base(DefaultCode, string.Format(DefaultDescriptionFormat, attemptedOperation), inner) {
        }
    }
}
