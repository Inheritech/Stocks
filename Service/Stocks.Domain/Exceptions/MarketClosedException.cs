using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Exception thrown when an operation is attempted to be called while the market is closed.
    /// </summary>
    [Serializable]
    public class MarketClosedException : DomainException {

        /// <summary>
        /// Business rule violation code for this kind of exception.
        /// </summary>
        private const string DefaultCode = "CLOSE_MARKET";

        /// <summary>
        /// Human-readable description to show for this kind of exception.
        /// </summary>
        private const string DefaultDescription = "The operation could not be completed because the market is currently closed.";

        public MarketClosedException() : base(DefaultCode, DefaultDescription) {
        }

        public MarketClosedException(Exception inner) : base(DefaultCode, DefaultDescription, inner) {
        }
    }
}
