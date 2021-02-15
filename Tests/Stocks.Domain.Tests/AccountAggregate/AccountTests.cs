using NUnit.Framework;
using Stocks.Domain.Aggregates.AccountAggregate;
using Stocks.Domain.Exceptions;

namespace Stocks.Domain.Tests.AccountAggregate {

    [TestFixture]
    public class AccountTests {

        [Test]
        public void ConstructorWorksWithValidParameters() {
            // Arrange
            static void create() => new Account(10);

            // Act/Assert
            Assert.DoesNotThrow(create);
        }

        [Test]
        public void ConstructorThrowsWhenInitialBalanceIsNegative() {
            // Arrange
            static void create() => new Account(-1);

            // Act/Assert
            Assert.Throws<InvalidInitialBalanceException>(create);
        }

        [Test]
        public void DepositIncreasesTheAppropriateCashAmount() {
            // Arrange
            var account = new Account(10);

            // Act
            account.Deposit(30);

            // Assert
            Assert.AreEqual(10 + 30, account.Cash);
        }

        [Test]
        public void DepositThrowsWhenAmountIsNegative() {
            // Arrange
            var account = new Account(10);
            void deposit() => account.Deposit(-10);

            // Act/Assert
            Assert.Throws<InvalidAccountOperationException>(deposit);
        }

        [Test]
        public void DeductReducesTheAppropriateCashAmount() {
            // Arrange
            var account = new Account(20);

            // Act
            account.Deduct(10);

            // Assert
            Assert.AreEqual(20 - 10, account.Cash);
        }

        [Test]
        public void DeductThrowsWhenAmountIsNegative() {
            // Arrange
            var account = new Account(20);
            void deduct() => account.Deduct(-10);

            // Act/Assert
            Assert.Throws<InvalidAccountOperationException>(deduct);
        }

        [Test]
        public void DeductThrowsWhenAccountBalanceIsInsufficient() {
            // Arrange
            var account = new Account(10);
            void deduct() => account.Deduct(20);

            // Act/Assert
            Assert.Throws<InsufficientBalanceException>(deduct);
        }
    }
}
