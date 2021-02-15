using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when an attempted operation on an account is not valid for the current account state.
    /// </summary>
    [Serializable]
    public class InvalidAccountOperationException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_ACCOUNT_OPERATION";

        public InvalidAccountOperationException(string description) : base(DefaultCode, description) {
        }

        public InvalidAccountOperationException(string description, Exception inner) : base(DefaultCode, description, inner) {
        }
    }
}
