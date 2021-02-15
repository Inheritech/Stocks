using NUnit.Framework;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Exceptions;
using System;

namespace Stocks.Domain.Tests.TransactionAggregate {
    
    [TestFixture]
    public class TransactionTests {

        public static readonly DateTime ValidTransactionTime = new DateTime(2021, 1, 1, 12, 0, 0);
        public static readonly DateTime InvalidTransactionTime = new DateTime(2021, 1, 1, 0, 0, 0);

        [Test]
        public void ConstructorWorksWithValidParameters() {
            // Arrange
            var account = new Account(10);
            void create() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                80
            );

            // Act/Assert
            Assert.DoesNotThrow(create);
        }

        [Test]
        public void ConstructorThrowsWhenAccountIsNull() {
            // Arrange
            static void create() => new Transaction(
                null,
                ValidTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                80
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(create);
        }

        [Test]
        public void ConstructorThrowsWhenOperationIsNull() {
            // Arrange
            var account = new Account(10);
            void create() => new Transaction(
                account,
                ValidTransactionTime,
                null,
                "NTFX",
                1,
                80
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(create);
        }

        [Test]
        public void ConstructorThrowsWhenMarketIsClosed() {
            // Arrange
            var account = new Account(10);
            void create() => new Transaction(
                account,
                InvalidTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                80
            );

            // Act/Assert
            Assert.Throws<MarketClosedException>(create);
        }

        [Test]
        public void ConstructorThrowsWhenSharesAreZeroOrLess() {
            // Arrange
            var account = new Account(10);
            void createZero() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "NTFX",
                0,
                10
            );
            void createNegative() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "NTFX",
                -1,
                10
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(createZero);
            Assert.Throws<InvalidTransactionException>(createNegative);
        }

        [Test]
        public void ConstructorThrowsWhenSharePriceIsZeroOrLess() {
            // Arrange
            var account = new Account(10);
            void createZero() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                0
            );

            void createNegative() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                0
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(createZero);
            Assert.Throws<InvalidTransactionException>(createNegative);
        }

        public void ConstructorThrowsWhenIssuerIsNullOrWhitespace() {
            // Arrange
            var account = new Account(10);
            void createNull() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                null,
                1,
                1
            );
            void createEmpty() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "",
                1,
                1
            );
            void createWhitespace() => new Transaction(
                account,
                ValidTransactionTime,
                Operation.Buy,
                "  ",
                1,
                1
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(createNull);
            Assert.Throws<InvalidTransactionException>(createEmpty);
            Assert.Throws<InvalidTransactionException>(createWhitespace);
        }
    }
}
