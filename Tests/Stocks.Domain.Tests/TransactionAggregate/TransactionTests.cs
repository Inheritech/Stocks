using NUnit.Framework;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Aggregates.TransactionAggregate;
using Stocks.Domain.Exceptions;
using System;

namespace Stocks.Domain.Tests.TransactionAggregate {
    
    [TestFixture]
    public class TransactionTests {

        private static readonly DateTime _validTransactionTime = new DateTime(2021, 1, 1, 12, 0, 0);
        private static readonly DateTime _invalidTransactionTime = new DateTime(2021, 1, 1, 0, 0, 0);

        [Test]
        public void ConstructorWorksWithValidParameters() {
            // Arrange
            var account = new Account(10);
            void create() => new Transaction(
                account,
                _validTransactionTime,
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
                _validTransactionTime,
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
                _validTransactionTime,
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
                _invalidTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                80
            );

            // Act/Assert
            Assert.Throws<MarketClosedException>(create);
        }

        [Test]
        public void ConstructorThrowsWhenSharesAreNegative() {
            // Arrange
            var account = new Account(10);
            void create() => new Transaction(
                account,
                _validTransactionTime,
                Operation.Buy,
                "NTFX",
                -1,
                10
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(create);
        }

        [Test]
        public void ConstructorThrowsWhenSharePriceIsZeroOrLess() {
            // Arrange
            var account = new Account(10);
            void createZero() => new Transaction(
                account,
                _validTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                0
            );

            void createNegative() => new Transaction(
                account,
                _validTransactionTime,
                Operation.Buy,
                "NTFX",
                1,
                0
            );

            // Act/Assert
            Assert.Throws<InvalidTransactionException>(createZero);
            Assert.Throws<InvalidTransactionException>(createNegative);
        }
    }
}
