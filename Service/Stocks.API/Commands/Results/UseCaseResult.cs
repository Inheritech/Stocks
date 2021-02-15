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
        public IReadOnlyList<string> BusinessErrors { get; }

        public UseCaseResult(IEnumerable<string> businessErrors) {
            BusinessErrors = businessErrors.ToList();
        }

        /// <summary>
        /// Check if the result is error free
        /// </summary>
        public bool IsSuccess() => !BusinessErrors.Any();
    }
}
