using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks.Domain.Common {
    /// <summary>
    /// Represents a persistable unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable {
        /// <summary>
        /// Save domain entities to a persistence storage.
        /// </summary>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
