namespace Stocks.Domain.Common {
    public interface IRepository<T> : IRepository where T : IAggregateRoot {
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IRepository { }
}
