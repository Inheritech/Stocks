using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Aggregates.TransactionAggregate;

namespace Stocks.Infrastructure.EntityConfigurations {
    class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction> {

        private readonly string _schema;

        public TransactionEntityConfiguration(string schema = null) {
            _schema = schema;
        }

        public void Configure(EntityTypeBuilder<Transaction> builder) {
            const string tableName = "Transactions";
            if (_schema is null)
                builder.ToTable(tableName);
            else
                builder.ToTable(tableName, _schema);

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .UseHiLo("TransactionsSequence", _schema);

            builder.Property(_ => _.AccountId);
            builder.Property(_ => _.Timestamp)
                .IsRequired();
            builder.Property(_ => _.Issuer)
                .IsRequired();
            builder.Property(_ => _.Shares);
            builder.Property(_ => _.SharePrice);
            builder.HasOne(_ => _.Operation)
                .WithMany();
        }
    }
}
