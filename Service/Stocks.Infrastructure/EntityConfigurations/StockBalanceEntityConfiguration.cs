using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Aggregates.AccountAggregate;

namespace Stocks.Infrastructure.EntityConfigurations {
    class StockBalanceEntityConfiguration : IEntityTypeConfiguration<StockBalance> {

        private readonly string _schema;

        public StockBalanceEntityConfiguration(string schema = null) {
            _schema = schema;
        }

        public void Configure(EntityTypeBuilder<StockBalance> builder) {
            const string tableName = "StockBalances";
            if (_schema is null)
                builder.ToTable(tableName);
            else
                builder.ToTable(tableName, _schema);

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .UseHiLo("StockBalancesSequence", _schema);

            builder.Property(_ => _.AccountId);
            builder.Property(_ => _.Issuer)
                .IsRequired();
            builder.Property(_ => _.Shares);
            builder.Property(_ => _.SharePrice);
        }
    }
}
