using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when the initial balance for an account is invalid.
    /// </summary>
    [Serializable]
    public class InvalidInitialBalanceException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INVALID_INITIAL_BALANCE";

        /// <summary>
        /// Human-readable description to show for this kind of exception.
        /// </summary>
        private const string DefaultDescription = "The provided initial balance is not valid for a new account.";

        public InvalidInitialBalanceException() : base(DefaultCode, DefaultDescription) {
        }

        public InvalidInitialBalanceException(Exception inner) : base(DefaultCode, DefaultDescription, inner) {
        }
    }
}
