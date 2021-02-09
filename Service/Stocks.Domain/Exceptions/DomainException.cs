using System;

namespace Stocks.Domain.Exceptions {

    /// <summary>
    /// Base class for domain exceptions.
    /// </summary>
    [Serializable]
    public abstract class DomainException : Exception {
        /// <summary>
        /// Code of the domain exception or business rule violation.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Human-readable description of the error or violation.
        /// </summary>
        public string Description { get; }

        public DomainException(string code, string description) : base(description) {
            Code = code;
            Description = description;
        }
        public DomainException(string code, string description, Exception inner) : base(description, inner) {
            Code = code;
            Description = description;
        }
        protected DomainException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
