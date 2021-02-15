using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when the information provided for an account does not match
    /// an existing account
    /// </summary>
    [Serializable]
    public class AccountNotFoundException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "ACCOUNT_NOT_FOUND";

        /// <summary>
        /// Human-readable description to show for this kind of exception.
        /// </summary>
        private const string DefaultDescription = "The requested account could not be found.";

        public AccountNotFoundException() : base(DefaultCode, DefaultDescription) {
        }

        public AccountNotFoundException(Exception inner) : base(DefaultCode, DefaultDescription, inner) {
        }
    }
}
