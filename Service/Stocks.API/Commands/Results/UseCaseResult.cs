using System;
using System.Collections.Generic;
using System.Linq;

namespace Stocks.API.Commands.Results {

    /// <summary>
    /// Shared base class for results that need to return data even when business errors occur
    /// </summary>
    public abstract class UseCaseResult {

        /// <summary>
        /// Business errors that occured while executing a use case
        /// </summary>
        public IReadOnlyList<string> BusinessErrors { get => _businessErrors; } 

        private List<string> _businessErrors;

        public UseCaseResult() {
            _businessErrors = new List<string>();
        }

        /// <summary>
        /// Check if the result is error free
        /// </summary>
        public bool IsSuccess() => !BusinessErrors.Any();

        public void AddBusinessError(string businessErrorCode) {
            if (businessErrorCode is null)
                throw new ArgumentNullException(nameof(businessErrorCode));
            _businessErrors.Add(businessErrorCode);
        }
    }
}
