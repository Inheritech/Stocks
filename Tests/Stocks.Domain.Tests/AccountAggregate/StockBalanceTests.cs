using NUnit.Framework;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Exceptions;

namespace Stocks.Domain.Tests.AccountAggregate {

    [TestFixture]
    public class StockBalanceTests {

        [Test]
        public void ConstructorWorksWithValidParameters() {
            // Arrange
            var account = new Account(10);
            void create() => new StockBalance(
                account,
                "NTFX"
            );

            // Act/Assert
            Assert.DoesNotThrow(create);
        }

        [Test]
        public void ConstructorThrowsWhenAccountIsNull() {
            // Arrange
            static void create() => new StockBalance(
                null,
                "NTFX"
            );

            // Act/Assert
            Assert.Throws<InvalidStockBalanceException>(create);
        }

        [Test]
        public void ConstructorThrowsWhenIssuerIsNullOrWhitespace() {
            // Arrange
            var account = new Account(10);
            void createNull() => new StockBalance(
                account,
                null
            );
            void createEmpty() => new StockBalance(
                account,
                ""
            );
            void createWhitespace() => new StockBalance(
                account,
                "  "
            );

            // Act/Assert
            Assert.Throws<InvalidStockBalanceException>(createNull);
            Assert.Throws<InvalidStockBalanceException>(createEmpty);
            Assert.Throws<InvalidStockBalanceException>(createWhitespace);
        }

        [Test]
        public void AddSharesThrowsWhenAmountIsZeroOrLess() {
            // Arrange
            var account = new Account(10);
            var shareBalance = new StockBalance(account, "NTFX");
            void addZero() => shareBalance.AddShares(0, 10);
            void addNegative() => shareBalance.AddShares(-1, 10);

            // Act/Assert
            Assert.Throws<InvalidStockBalanceOperationException>(addZero);
            Assert.Throws<InvalidStockBalanceOperationException>(addNegative);
        }

        [Test]
        public void AddSharesThrowsWhenPriceIsZeroOrLess() {
            // Arrange
            var account = new Account(10);
            var shareBalance = new StockBalance(account, "NTFX");

            // Act
            void addWithZero() => shareBalance.AddShares(10, 0);
            void addWithNegative() => shareBalance.AddShares(10, -1);

            // Assert
            Assert.Throws<InvalidStockBalanceOperationException>(addWithZero);
            Assert.Throws<InvalidStockBalanceOperationException>(addWithNegative);
        }

        [Test]
        public void AddSharesIncreasesAmountCorrectly() {
            // Arrange
            var account = new Account(10);
            var shareBalance = new StockBalance(account, "NTFX");

            // Act
            shareBalance.AddShares(15, 10);

            // Assert
            Assert.AreEqual(15, shareBalance.Shares);
        }

        [Test]
        public void AddSharesAveragesThePriceAppropriately() {
            // Arrange
            var account = new Account(10);
            var shareBalance1 = new StockBalance(account, "NTFX");
            var shareBalance2 = new StockBalance(account, "ADA");

            // Act
            shareBalance1.AddShares(10, 10);
            shareBalance1.AddShares(5, 7);
            shareBalance2.AddShares(2, 15.5m);
            shareBalance2.AddShares(1, 5);

            // Assert
            Assert.AreEqual(9, shareBalance1.SharePrice);
            Assert.AreEqual(12, shareBalance2.SharePrice);
        }

        [Test]
        public void SubtractSharesDecreasesAmountCorrectly() {
            // Arrange
            var account = new Account(10);
            var shareBalance = new StockBalance(account, "ADA");
            shareBalance.AddShares(10, 10);

            // Act
            shareBalance.SubtractShares(5);

            // Assert
            Assert.AreEqual(5, shareBalance.Shares);
        }

        [Test]
        public void SubtractSharesThrowsWhenAmountIsZeroOrLess() {
            // Arrange
            var account = new Account(10);
            var shareBalance = new StockBalance(account, "NTFX");
            shareBalance.AddShares(10, 10);

            // Act
            void addWithZero() => shareBalance.SubtractShares(0);
            void addWithNegative() => shareBalance.SubtractShares(-1);

            // Assert
            Assert.Throws<InvalidStockBalanceOperationException>(addWithZero);
            Assert.Throws<InvalidStockBalanceOperationException>(addWithNegative);
        }
    }
}
