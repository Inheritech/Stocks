using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Aggregates.TransactionAggregate;

namespace Stocks.Infrastructure.EntityConfigurations {
    class OperationEntityConfiguration : IEntityTypeConfiguration<Operation> {

        private readonly string _schema;

        public OperationEntityConfiguration(string schema = null) {
            _schema = schema;
        }

        public void Configure(EntityTypeBuilder<Operation> builder) {
            const string tableName = "Operation";
            if (_schema is null)
                builder.ToTable(tableName);
            else
                builder.ToTable(tableName, _schema);

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(_ => _.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(Operation.List());
        }
    }
}
