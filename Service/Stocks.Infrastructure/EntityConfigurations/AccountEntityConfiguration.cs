using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Aggregates.AccountAggregate;

namespace Stocks.Infrastructure.EntityConfigurations {
    class AccountEntityConfiguration : IEntityTypeConfiguration<Account> {

        private readonly string _schema;

        public AccountEntityConfiguration(string schema = null) {
            _schema = schema;
        }

        public void Configure(EntityTypeBuilder<Account> builder) {
            const string tableName = "Accounts";
            if (_schema is null)
                builder.ToTable(tableName);
            else
                builder.ToTable(tableName, _schema);

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .UseHiLo("AccountsSequence", _schema);

            builder.Property(_ => _.Cash);
            builder.Metadata.FindNavigation(nameof(Account.StockBalances))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
