using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when the balance of an account is not enough to make a deduction.
    /// </summary>
    [Serializable]
    public class InsufficientBalanceException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "INSUFFICIENT_BALANCE";

        /// <summary>
        /// Human-readable description to show for this kind of exception.
        /// </summary>
        private const string DefaultDescription = "The available balance is not enough to make the requested deduction.";

        public InsufficientBalanceException() : base(DefaultCode, DefaultDescription) {
        }

        public InsufficientBalanceException(Exception inner) : base(DefaultCode, DefaultDescription, inner) {
        }
    }
}
